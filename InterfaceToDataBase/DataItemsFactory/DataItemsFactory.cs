using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Data.Common;

namespace InterfaceToDataBase
{
    public abstract class DataItemsFactory
    {
        public abstract List<String> FirstTableFields { get; }
        public abstract List<String> OtherTableFields { get; }
        public abstract List<String> SearchTableFields { get; }
        public abstract string TableName { get; }
        public abstract DataItem GetEmptyDataItem();
        public abstract DataItem GetDataItemDefault();
        public DataItem GetFirstFilledDataItem(int Id)
        {
            var dataItem = GetEmptyDataItem();
            dataItem.Id = Id;
            dataItem.Fill(FirstTableFields);
            return dataItem;
        }

    }
}
