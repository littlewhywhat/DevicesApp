using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InterfaceToDataBase;

namespace InterfaceToClient
{
    public class CompanyIdComboBox : DeviceExtraComboBox
    {
        public CompanyIdComboBox() : base(TableNames.Companies)
        { }

        protected override DataItemController GetCurrentSelection()
        {
            return DeviceControllerDataContext.Company;
        }

        protected override IEnumerable<DataItemController> GetControllersWithoutEmpty()
        {
            return DeviceControllerDataContext.GetCompanies();
        }

        protected override void ChangeCurrentDataContextBySelectedItem()
        {
            DeviceControllerDataContext.Company = (CompanyController)CurrentSelectedItem;
        }


        protected override DataItemController GetEmptyController()
        {
            var controller = FactoriesVault.Dic[TableNames.Companies].Factory.GetControllerEmpty();
            controller.Name = "Без компании";
            return controller;
        }
    }
}
