using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace MiaMain
{
    public class CompaniesFactory : DataItemsFactory
    {
        private List<string> firstTableFields = new List<String> { "Id", "CompanyName" };
        private List<string> otherTableFields = new List<String> { "CompanyInfo" };
        private string tableName = "Companies";
        public override List<string> FirstTableFields { get { return firstTableFields; } }
        public override List<string> OtherTableFields { get { return otherTableFields; } }
        public override string TableName { get { return tableName; } }
        
        public CompaniesFactory(SqlConnection connection) : base (connection)
        { }
        public override DataItem GetDataItem()
        {
            return new Company();
        }
    }
}
