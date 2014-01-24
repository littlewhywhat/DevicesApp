using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Data.Common;

namespace MiaMain
{
    public abstract class DataItemsFactory
    {
        public abstract List<String> FirstTableFields { get; }
        public abstract List<String> OtherTableFields { get; }
        public abstract List<String> SearchTableFields { get; }
        public abstract string TableName { get; }
        public abstract DataItem GetEmptyDataItem();
        private ObservableDictionary<int, DataItem> DataItemsDic = new ObservableDictionary<int, DataItem>();
        public ObservableDictionary<int, DataItem> GetDataItemsDic()
        {
            return DataItemsDic;
        }
        

        public DataItem GetFilledDataItem(int Id)
        {
            var dataItem = GetEmptyDataItem();
            dataItem.Id = Id;
            dataItem.Fill(FirstTableFields);
            return dataItem;
        }

        protected virtual void DeleteReferences(DataItem dataItem, DbTransaction transaction)
        {
            dataItem.Factory.GetDataItemsDic().Select(keyValuePair => keyValuePair.Value).Where(dataItemInDic => dataItemInDic.ParentId == dataItem.Id)
                .ForEach(dataItemInDic =>
                {
                    new FillDataItem(dataItemInDic, dataItemInDic.Factory.OtherTableFields).Act(transaction.Connection);
                    dataItemInDic.ParentId = 0;
                    new UpdateDataItem(dataItemInDic).PerformTransaction(transaction);
                });
        }

        public void DeleteTransaction(DataItem dataItem, DbTransaction transaction)
        {
            DeleteReferences(dataItem, transaction);
            new DeleteDataItemRow(dataItem).PerformTransaction(transaction);
        }
    }
}
