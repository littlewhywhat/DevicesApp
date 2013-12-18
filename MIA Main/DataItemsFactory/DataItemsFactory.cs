using System;
using System.Collections.Generic;

namespace MiaMain
{
    public abstract class DataItemsFactory
    {
        private Dictionary<int, DataItem> DataItemsDic = new Dictionary<int, DataItem>();
        public abstract List<String> FirstTableFields { get; }
        public abstract List<String> OtherTableFields { get; }
        public abstract string TableName { get; }
        public Dictionary<int, DataItem> GetDataItemsDic()
        {
            return DataItemsDic;
        }
        public abstract DataItem GetDataItem();
    }
}
