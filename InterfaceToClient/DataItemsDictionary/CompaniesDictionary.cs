using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InterfaceToDataBase;

namespace InterfaceToClient
{
    public class CompaniesDictionary : DataItemControllersDictionary
    {
        public CompaniesDictionary()
        { }

        protected override DataItemControllersFactory GetFactory()
        {
            return new CompanyControllersFactory();
        }
    }
}
