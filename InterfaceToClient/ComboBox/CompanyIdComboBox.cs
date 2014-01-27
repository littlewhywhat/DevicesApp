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

        protected override IEnumerable<DataItemController> GetControllers()
        {
            return DeviceControllerDataContext.GetCompanies();
        }

        protected override void ChangeCurrentDataContextBySelectedItem()
        {
            DeviceControllerDataContext.Company = CurrentSelectedItem;
        }

    }
}
