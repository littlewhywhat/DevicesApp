using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataItemsLibrary
{
    public class DeviceTypesFactory : DataItemsFactory
    {
        private List<string> firstTableFields = new List<String> { "Id", "Name", "ParentId", "IsMarker", "FullName", "IVUK"};
        private List<string> otherTableFields = new List<String> { "Name"};
        private List<string> searchTableFields = new List<String> { "Name", "Marker", "FullName", "IVUK" };

        public override List<string> FirstTableFields { get { return firstTableFields; } }
        public override List<string> OtherTableFields { get { return otherTableFields; } }
        public override List<string> SearchTableFields { get { return searchTableFields; } }
        public override string TableName { get { return TableNames.DeviceTypes; } }

        public override DataItem GetEmptyDataItem()
        {
            return new DeviceType(this);
        }

        public override DataItem GetDataItemDefault()
        {
            var dataItem = (DeviceType)GetEmptyDataItem();
            dataItem.Name = "Новый тип";
            return dataItem;
        }
    }
}
