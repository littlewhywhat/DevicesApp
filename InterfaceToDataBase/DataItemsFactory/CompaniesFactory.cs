using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Common;

namespace InterfaceToDataBase
{
    public class CompaniesFactory : DataItemsFactory
    {
        private List<string> firstTableFields = new List<String> { "Id", "Name", "ParentId", "Info" };
        private List<string> otherTableFields = new List<String> { "Info" };
        private List<string> searchTableFields = new List<String> { "Info", "Name" };
        public override List<string> FirstTableFields { get { return firstTableFields; } }
        public override List<string> OtherTableFields { get { return otherTableFields; } }
        public override List<string> SearchTableFields { get { return searchTableFields; } }
        public override string TableName { get { return TableNames.Companies; } }
        
        public override DataItem GetEmptyDataItem()
        {
            return new Company(this);
        }

        public override DataItem GetDataItemDefault()
        {
            var dataItem = (Company)GetEmptyDataItem();
            dataItem.Name = "Новая компания";
            return dataItem;
        }
    }
}
