using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataItemsLibrary;

namespace DataItemControllersLibrary
{
    public class CompaniesDictionary : DataItemControllersWithParentsDictionary
    {
        public CompaniesDictionary(DictionariesVault vault) : base(vault)
        { }
        public CompaniesDictionary(CompanyControllersFactory factory) : base(factory)
        { }
        const string _UndefinedCompany = "Компания неопределена";
        private CompanyController UndefinedCompany;
        private CompanyController InitUndefinedCompany()
        {
            var controller = (CompanyController)Factory.GetControllerEmpty();
            controller.Name = _UndefinedCompany;
            return controller;
        }

        protected override DataItemControllersFactory GetFactory(DictionariesVault vault)
        {
            return new CompanyControllersFactory(vault);
        }

        internal CompanyController GetUndefinedCompany()
        {
            if (UndefinedCompany == null)
                UndefinedCompany = InitUndefinedCompany();
            return UndefinedCompany;
        }
    }
}
