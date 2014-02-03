using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataItemsLibrary;

namespace DataItemControllersLibrary
{
    public class CompaniesDictionary : DataItemControllersWithParentsDictionary
    {
        private CompanyController UndefinedCompany;
        public CompaniesDictionary(DictionariesVault vault) : base(vault)
        { }
        public CompaniesDictionary(CompanyControllersFactory factory) : base(factory)
        { }

        protected override DataItemControllersFactory GetFactory(DictionariesVault vault)
        {
            return new CompanyControllersFactory(vault);
        }

        const string _UndefinedCompany = "Компания неопределена";
        private CompanyController InitUndefinedCompany()
        {
            var controller = (CompanyController)Factory.GetControllerEmpty();
            controller.Name = _UndefinedCompany;
            return controller;
        }

        public CompanyController GetUndefinedCompany()
        {
            if (UndefinedCompany == null)
                UndefinedCompany = InitUndefinedCompany();
            return UndefinedCompany;
        }
    }
}
