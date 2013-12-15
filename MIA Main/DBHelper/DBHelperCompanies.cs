﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIA_Main
{
    class DBHelperCompanies : DBHelper
    {
        public DBHelperCompanies(string connectionString) : base(connectionString)
        {
            TableName = "Companies";
        }
        protected override DataItem GetDataItem()
        {
            return new Company();
        }
    }
}