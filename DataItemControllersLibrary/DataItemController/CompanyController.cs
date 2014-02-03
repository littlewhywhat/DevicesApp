using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataItemsLibrary;
using DBActionLibrary;

namespace DataItemControllersLibrary
{
    public class CompanyController : DataItemControllerWithParents
    {
        public CompanyController(Company company, CompanyControllersFactory factory) : base(company, factory)
        { }
        private DevicesDictionary DevicesDic { get { return Factory.Vault.DevicesDic; } }
        private Company Company { get { return (Company)DataItem; } }
        private IEnumerable<DeviceController> GetReferencedDevices()
        {
            return DevicesDic.GetDevicesWithCompanyId(Company.Id);
        }
        private IEnumerable<DataItemAction> GetDevicesDeleteReferences()
        {
            return GetReferencedDevices().Select(deviceController => deviceController.GetActionForDeleteCompanyReference());
        }

        const string _Info = "Info";
        protected override void OnPropertyChanged()
        {
            base.OnPropertyChanged();
            OnPropertyChanged(_Info);
        }
        protected override bool IsTheSameByType(DataItemController controller)
        {
            return controller is CompanyController;
        }
        protected override List<DataItemAction> GetDeleteReferencesActions()
        {
            var actions = base.GetDeleteReferencesActions();
            actions.AddRange(GetDevicesDeleteReferences());
            return actions;
        }
        
        public string Info { get { return Company.Info; } set { Company.Info = value; OnPropertyChanged(); } }
    }
}
