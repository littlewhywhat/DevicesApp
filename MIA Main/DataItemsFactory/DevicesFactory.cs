using System;
using System.Collections.Generic;

namespace MiaMain
{
    public class DevicesFactory : DataItemsFactory
    {
        private List<string> firstTableFields = new List<String> { "Id", "Name", "ParentId" };
        private List<string> otherTableFields = new List<String> { "Info","CompanyId" };
        private string tableName = "Devices";
        
        public override List<string> FirstTableFields { get { return firstTableFields; } }
        public override List<string> OtherTableFields { get { return otherTableFields; } }
        public override string TableName { get { return tableName; } }

        public override DataItem GetDataItem()
        {
            return new Device(this);
        }
    }
}
