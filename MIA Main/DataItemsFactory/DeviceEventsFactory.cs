using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiaMain
{
    public class DeviceEventsFactory : DataItemsFactory
    {
        private List<string> firstTableFields = new List<String> { "Id", "Name", "ParentId", "DeviceId", "Type", "Date" };
        private List<string> otherTableFields = new List<String> {  };
        private string tableName = "DeviceEvents";

        public override List<string> FirstTableFields { get { return firstTableFields; } }

        public override List<string> OtherTableFields { get { return otherTableFields; } }

        public override string TableName { get { return tableName; } }

        public override DataItem GetEmptyDataItem()
        {
            return new DeviceEvent(this);
        }
    }
}
