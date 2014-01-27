using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Data.SqlClient;

namespace InterfaceToDataBase
{
    static class Program
    {
        public static void Main()
        {
            using (var connection = new SqlConnection("Data Source=" + "WHYWHAT-PC\\SQLEXPRESS" + "; Integrated Security = SSPI; Initial Catalog=" + "MiaDB"))
            {
                //Update Test
                //var companiesFactory = new CompaniesFactory();
                //DBHelper.PerformDBAction(connection, new FillDataDic(companiesFactory));
                //var companiesDic = companiesFactory.DataItemsDic;
                //Company company = (Company)companiesDic[7];
                //company.Info = "Info 7";
                //company.Name = "Company 7";
                //DBHelper.PerformDBAction(connection, new UpdateDataItem(companiesFactory, company));
                //Insert Test
                //var companiesFactory = new CompaniesFactory();
                //var company = (Company)companiesFactory.GetDataItem();
                //DBHelper.PerformDBAction(connection, new GetNewDataItemId(companiesFactory, company));
                //company.Info = "Test";
                //company.Name = "CompanyTest";
                //DBHelper.PerformDBAction(connection, new InsertDataItem(companiesFactory, company));
                //Delete Test
                //var companiesFactory = new CompaniesFactory();
                //var company = companiesFactory.GetDataItem();
                //company.Id = 8;
                //DBHelper.PerformDBAction(connection, new DeleteDataItem(companiesFactory, company));
                
            };
        }
    }
}
