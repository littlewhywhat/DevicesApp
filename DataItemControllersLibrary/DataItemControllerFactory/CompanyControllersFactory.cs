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
        protected override DataItemsFactory GetFactory()
        {
            return new CompaniesFactory();
        }

        internal override DataItemController GetController(DataItem dataItem)
        {
            return new CompanyController((Company)dataItem, this);
        }
    }
}
