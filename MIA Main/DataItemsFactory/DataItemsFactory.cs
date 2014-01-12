using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MiaMain
{
    public abstract class DataItemsFactory
    {
        public abstract List<String> FirstTableFields { get; }
        public abstract List<String> OtherTableFields { get; }
        public abstract string TableName { get; }
        public abstract DataItem GetDataItem();
        private ObservableDictionary<int, DataItem> DataItemsDic = new ObservableDictionary<int, DataItem>();
        public ObservableDictionary<int, DataItem> GetDataItemsDic()
        {
            return DataItemsDic;
        }
        public DataItem GetFilledDataItem(int Id)
        {
            var dataItem = GetDataItem();
            dataItem.Id = Id;
            dataItem.Fill(FirstTableFields);
            return dataItem;
        }
    }
}
