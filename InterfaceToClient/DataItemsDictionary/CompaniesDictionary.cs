using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InterfaceToDataBase;

namespace InterfaceToClient
{
    public class CompaniesDictionary : DataItemsDictionary
    {
        public CompaniesDictionary(CompaniesFactory factory) : base(factory)
        { }
    }
}
