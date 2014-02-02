using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataItemsLibrary;

namespace InterfaceToClient
{
    public class CompaniesDictionary : DataItemControllersWithParentsDictionary
    {
        private CompanyController UndefinedCompany;
        public CompaniesDictionary()
        { }

        protected override DataItemControllersFactory GetFactory()
        {
            return new CompanyControllersFactory();
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
