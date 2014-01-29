using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InterfaceToDataBase;

namespace InterfaceToClient
{
    public class DeviceController : DataItemController
    {
        public DeviceController(DataItem dataItem, DataItemControllersFactory factory) : base(dataItem, factory) { }
        protected override DataItemControllersDictionary GetDictionary()
        {
            return FactoriesVault.Dic[Factory.TableName];
        }
        private Device Device { get { return (Device)DataItem; } }

        #region Dictionaries
        private DeviceTypesDictionary DeviceTypesDic { get { return (DeviceTypesDictionary)FactoriesVault.Dic[TableNames.DeviceTypes]; } }
        private CompaniesDictionary CompaniesDic { get { return (CompaniesDictionary)FactoriesVault.Dic[TableNames.Companies]; } }
        private DeviceEventsDictionary DeviceEventsDic { get { return (DeviceEventsDictionary)FactoriesVault.Dic[TableNames.DeviceEvents]; } }
        #endregion

        const string _Type = "Type";
        const string _Company = "Company";
        protected override void OnPropertyChanged()
        {
            base.OnPropertyChanged();
            OnPropertyChanged(_Type);
            OnPropertyChanged(_Company);
        }
        public bool HasType { get { return Device.TypeId != 0; } }
        public DeviceTypeController Type
        {
            get { return (DeviceTypeController)DeviceTypesDic.GetDataItemControllerById(Device.TypeId); }
            set
            {
                Device.TypeId = value.Id;
                OnPropertyChanged();
            }
        }
        public CompanyController Company
        {
            get { return (CompanyController)CompaniesDic.GetDataItemControllerById(Device.CompanyId); }
            set
            {
                Device.CompanyId = value.Id;
                OnPropertyChanged();
            }
        }

        public bool HasTheSameCompanyId(int Id) { return Device.CompanyId == Id; }
        public bool HasTheSameTypeId(int Id) { return Device.TypeId == Id; }

        public IEnumerable<DataItemController> GetTypes()
        {
            return DeviceTypesDic.GetTypesWithoutMarker();
        }

        public IEnumerable<DataItemController> GetCompanies()
        {
            return CompaniesDic.DataItemControllersDic.Values;
        }

        public IEnumerable<DataItemController> GetEvents()
        {
            return DeviceEventsDic.GetEventsByDeviceId(Id);
        }

        public UpdateDataItem DeleteCompanyTransaction()
        {
            var clone = (Device)DataItem.Clone();
            clone.CompanyId = 0;
            return new UpdateDataItem(clone);
        }

        public UpdateDataItem DeleteTypeTransaction()
        {
            var clone = (Device)DataItem.Clone();
            clone.TypeId = 0;
            return new UpdateDataItem(clone);
        }

        protected override List<TransactionData> GetDeleteReferencesActions()
        {
            var actions = base.GetDeleteReferencesActions();
            actions.AddRange(DeviceEventsDic.GetEventsByDeviceId(Device.Id).Select(eventController =>
                (TransactionData)((DeviceEventController)eventController).DeleteDeviceTransaction()).ToList());
            return actions;
        }
        
        public override Dictionary<string, string> GetSearchPropertyValueDic()
        {
            var searchPropertyValueDic = base.GetSearchPropertyValueDic();
            if (Device.TypeId != 0)
                Type.GetSearchPropertyValueDic().ForEach(keyValuePair => searchPropertyValueDic.Add("Type" + keyValuePair.Key, keyValuePair.Value));
            return searchPropertyValueDic;
        }


        public override bool IsTheSameByType(DataItemController controller)
        {
            return controller is DeviceController;
        }

        public DeviceEventController GetNewEventController()
        {
            var eventController = (DeviceEventController)DeviceEventsDic.Factory.GetControllerDefault();
            eventController.Device = this;
            return eventController;
        }
    }
}
