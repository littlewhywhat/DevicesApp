using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace InterfaceToDataBase
{
    public class Company : DataItem
    {
        public string Info { get; set; }
        public Company(CompaniesFactory Factory) : base(Factory) { }

        public override bool IsTheSameType(DataItem dataItem)
        {
            return dataItem is Company;
        }
    }
}
