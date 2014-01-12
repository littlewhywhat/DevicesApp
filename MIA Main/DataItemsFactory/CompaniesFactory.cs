using System;
using System.Collections.Generic;

namespace MiaMain
{
    public class CompaniesFactory : DataItemsFactory
    {
        private List<string> firstTableFields = new List<String> { "Id", "Name" , "ParentId" };
        private List<string> otherTableFields = new List<String> { "Info" };
        private string tableName = "Companies";
        public override List<string> FirstTableFields { get { return firstTableFields; } }
        public override List<string> OtherTableFields { get { return otherTableFields; } }
        public override string TableName { get { return tableName; } }
        
        public override DataItem GetDataItem()
        {
            return new Company(this);
        }
    }
}
