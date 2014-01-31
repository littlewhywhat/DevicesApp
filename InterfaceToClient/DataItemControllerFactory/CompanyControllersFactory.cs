using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using InterfaceToDataBase;

namespace InterfaceToClient
{
    public class CompanyControllersFactory : DataItemControllersFactory
    {
        public CompanyControllersFactory()
        { }

        public override DataItemController GetController(DataItem dataItem)
        {
            return new CompanyController((Company)dataItem, this);
        }
        
        protected override DataItemsInfoGrid GetDataItemsInfoGrid()
        {
            return new CompaniesInfoGrid();
        }

        protected override DataItemsFactory GetFactory()
        {
            return new CompaniesFactory();
        }

    }
}
