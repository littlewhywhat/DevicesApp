using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataItemsLibrary;

namespace DataItemControllersLibrary
{
    public class CompanyControllersFactory : DataItemControllersFactory
    {
        public CompanyControllersFactory(DictionariesVault vault) : base(vault)
        { }

        public override DataItemController GetController(DataItem dataItem)
        {
            return new CompanyController((Company)dataItem, this);
        }
        protected override DataItemsFactory GetFactory()
        {
            return new CompaniesFactory();
        }
    }
}
