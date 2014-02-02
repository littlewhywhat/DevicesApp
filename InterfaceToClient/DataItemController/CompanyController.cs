using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataItemsLibrary;
using DBActionLibrary;

namespace InterfaceToClient
{
    public class CompanyController : DataItemControllerWithParents
    {
        public CompanyController(Company company, CompanyControllersFactory factory) : base(company, factory)
        { }
        protected override DataItemControllersDictionary GetDictionary()
        {
            return FactoriesVault.Dic[Factory.TableName];
        }
        const string _Info = "Info";
        protected override void OnPropertyChanged()
        {
            base.OnPropertyChanged();
            OnPropertyChanged(_Info);
        }

        public DevicesDictionary DevicesDic { get { return (DevicesDictionary)FactoriesVault.Dic[TableNames.Devices]; } }
        private Company Company { get { return (Company)DataItem; } }
        public string Info { get { return Company.Info; } set { Company.Info = value; OnPropertyChanged(); } }
        protected override List<DataItemAction> GetDeleteReferencesActions()
        {
            var actions = base.GetDeleteReferencesActions();
            actions.AddRange(GetDevicesDeleteReferences());
            return actions;
        }
        private IEnumerable<DeviceController> GetReferencedDevices()
        {
            return DevicesDic.GetDevicesWithCompanyId(Company.Id);
        }

        private IEnumerable<DataItemAction> GetDevicesDeleteReferences()
        {
            return GetReferencedDevices().Select(deviceController => deviceController.GetActionForDeleteCompanyReference());
        }

        public override bool IsTheSameByType(DataItemController controller)
        {
            return controller is CompanyController;
        }

    }
}
