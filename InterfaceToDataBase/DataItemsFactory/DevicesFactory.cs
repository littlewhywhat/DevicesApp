using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;

namespace InterfaceToDataBase
{
    public class DevicesFactory : DataItemsFactory
    {
        private List<string> firstTableFields = new List<String> { "Id", "Name", "ParentId", "CompanyId", "TypeId" };
        private List<string> otherTableFields = new List<String> { "Name" };
        private List<string> searchTableFields = new List<String> { "Name" };
        
        public override List<string> FirstTableFields { get { return firstTableFields; } }
        public override List<string> OtherTableFields { get { return otherTableFields; } }
        public override List<string> SearchTableFields { get { return searchTableFields; } }
        public override string TableName { get { return TableNames.Devices; } }

        public override DataItem GetEmptyDataItem()
        {
            return new Device(this);
        }


        public override DataItem GetDataItemDefault()
        {
            var dataItem = (Device)GetEmptyDataItem();
            dataItem.Name = "Новое изделие";
            return dataItem;
        }
    }
}
