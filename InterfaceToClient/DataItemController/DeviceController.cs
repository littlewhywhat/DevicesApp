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
        const string _TypeOrDefault = "TypeOrDefault";
        const string _Company = "Company";
        const string _CompanyOrDefault = "CompanyOrDefault";
        const string _Companies = "Companies";
        const string _Types = "Types";
        protected override void OnPropertyChanged()
        {
            base.OnPropertyChanged();
            OnPropertyChanged(_Type);
            OnPropertyChanged(_TypeOrDefault);
            OnPropertyChanged(_Company);
            OnPropertyChanged(_CompanyOrDefault);
            OnPropertyChanged(_Companies);
            OnPropertyChanged(_Types);
        }
        public override void CheckIds()
        {
            base.CheckIds();
            if (!CompanyExist)
                Device.CompanyId = 0;
            if (!TypeExist)
                Device.TypeId = 0;
        }

        public bool CompanyExist 
        { 
            get 
            {
                try { return GetCompanies().ToDictionary(company => company.Id).ContainsKey(Device.CompanyId); }
                catch { return false; }
            } 
        }
        public bool TypeExist
        {
            get
            {
                try { return GetTypes().ToDictionary(type => type.Id).ContainsKey(Device.TypeId); }
                catch { return false; }
            }
        }

        public bool HasType { get { return Device.TypeId != 0; } }
        public bool HasCompany { get { return Device.CompanyId != 0; } }
        public DeviceTypeController Type
        {
            get { return HasType? (DeviceTypeController)DeviceTypesDic.DataItemControllersDic[Device.TypeId] : null; }
            set
            {
                Device.TypeId = value.Id;
                OnPropertyChanged(_Type);
                OnPropertyChanged(_Parents);
            }
        }
        public DeviceTypeController TypeOrDefault
        {
            get { return HasType ? Type : DeviceTypesDic.GetUndefinedType(); }
            set { Type = value; }
        }

        public CompanyController Company
        {
            get { return HasCompany ? (CompanyController)CompaniesDic.DataItemControllersDic[Device.CompanyId] : null; }
            set
            {
                Device.CompanyId = value.Id;
                OnPropertyChanged(_CompanyOrDefault);
            }
        }
        public CompanyController CompanyOrDefault
        {
            get { return HasCompany ? Company : CompaniesDic.GetUndefinedCompany(); }
            set { Company = value; }
        }

        public bool HasTheSameCompanyId(int Id) { return Device.CompanyId == Id; }
        public bool HasTheSameTypeId(int Id) { return Device.TypeId == Id; }

        public List<DataItemController> Types
        {
            get
            {
                var result = GetTypes().ToList();
                result.Add(DeviceTypesDic.GetUndefinedType());
                if (!result.Contains(TypeOrDefault))
                    result.Add(TypeOrDefault);
                return result;
            }
        }

        public List<DataItemController> Companies
        {
            get
            {
                var result = GetCompanies().ToList();
                result.Add(CompaniesDic.GetUndefinedCompany());
                return result;
            }
        }


        public IEnumerable<DataItemController> GetTypes()
        {
            if (HasParents && ((DeviceController)Parent).HasType)
            {
                var result = DeviceTypesDic.GetChildrenDevicesTypes(((DeviceController)Parent).Type).ToList();
                GetDictionary().GetChildrenByParentId(Parent.Id).ToList().ForEach(child =>
                    {
                        if (result.Contains(((DataItemController)((DeviceController)child).Type)))
                            result.Remove(((DataItemController)((DeviceController)child).Type));
                    });
                return result;
            }
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
                Type.GetSearchPropertyValueDic().ToList().ForEach(keyValuePair => searchPropertyValueDic.Add("Type" + keyValuePair.Key, keyValuePair.Value));
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

        internal bool EventIsRelated(DeviceEventController dataItemController)
        {
            if (dataItemController != null)
                return this == dataItemController.Device;
            return false;
        }
    }
}
