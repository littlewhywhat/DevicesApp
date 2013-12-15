using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIA_Main
{
    static class Program
    {
        public static void Main()
        {      
            var dbHelper = new DBHelperCompanies("Data Source=" + "WHYWHAT-PC\\SQLEXPRESS" +
                "; Integrated Security = SSPI; Initial Catalog=" + "MiaDB");
            var devices = dbHelper.GetDataItemsDictionary(new List<string>() { "Id", "CompanyInfo", "CompanyName" });
        }
    }
}
