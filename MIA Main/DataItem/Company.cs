using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MiaMain
{
    public class Company : DataItem
    {
        public string Info { get; set; }
        public Company(CompaniesFactory Factory) : base(Factory) { } 
    }
}
