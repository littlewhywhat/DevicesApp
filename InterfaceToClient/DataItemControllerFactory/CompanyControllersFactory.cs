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
        public CompanyControllersFactory(CompaniesFactory factory) : base(factory)
        { }

        public override DataItemController GetController(DataItem dataItem)
        {
            return new CompanyController((Company)dataItem, this);
        }

        protected override DataItemsDictionary GetDataItemsDictionary(DataItemsFactory factory)
        {
            return new CompaniesDictionary((CompaniesFactory)factory);
        }

        protected override Grid GetTabItemContent()
        {
            var dataGrid = new TabItemGrid();
            dataGrid.ContentGrid = new CompaniesInfoGrid();
            return dataGrid;
        }

    }
}
