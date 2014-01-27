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

        private Company Company { get { return (Company)DataItem; } }
        protected override List<TransactionData> GetDeleteReferencesActions()
        {
            var actions = base.GetDeleteReferencesActions();
            actions.AddRange(((DeviceControllersFactory)FactoriesVault.Dic[TableNames.Devices]).
                GetDevicesWithCompanyId(Company.Id).Select(device =>
                {
                    new FillDataItem(device, device.Factory.OtherTableFields).Act(Connection.GetConnection());
                    ((Device)device).CompanyId = 0;
                    return (TransactionData)new UpdateDataItem(device);
                }));
            return actions;
        }

        public override bool IsTheSameByType(DataItemController controller)
        {
            return controller is CompanyController;
        }
    }
}
