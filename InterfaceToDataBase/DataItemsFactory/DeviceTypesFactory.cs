using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterfaceToDataBase
{
    public class DeviceTypesFactory : DataItemsFactory
    {
        private List<string> firstTableFields = new List<String> { "Id", "Name", "ParentId", "IsMarker"};
        private List<string> otherTableFields = new List<String> { "Name"};
        private List<string> searchTableFields = new List<String> { "Name", "Marker" };

        public override List<string> FirstTableFields { get { return firstTableFields; } }
        public override List<string> OtherTableFields { get { return otherTableFields; } }
        public override List<string> SearchTableFields { get { return searchTableFields; } }
        public override string TableName { get { return TableNames.DeviceTypes; } }

        public override DataItem GetEmptyDataItem()
        {
            return new DeviceType(this);
        }



    }
}
