using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InterfaceToDataBase;

namespace InterfaceToClient
{
    public class CompanyController : DataItemController
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

        public DevicesDictionary Dictionary { get { return (DevicesDictionary)GetDictionary(); } }
        private Company Company { get { return (Company)DataItem; } }
        public string Info { get { return Company.Info; } set { Company.Info = value; OnPropertyChanged(); } }
        protected override List<TransactionData> GetDeleteReferencesActions()
        {
            var actions = base.GetDeleteReferencesActions();
            actions.AddRange(Dictionary.GetDevicesWithCompanyId(Company.Id).Select(deviceController =>
                (TransactionData)((DeviceController)deviceController).DeleteCompanyTransaction()).ToList());
            return actions;
        }

        public override bool IsTheSameByType(DataItemController controller)
        {
            return controller is CompanyController;
        }

    }
}
