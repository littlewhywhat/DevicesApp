using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Data.Common;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace InterfaceToDataBase
{
    public class FillDataDic : GetData
    {
        protected DataItemsFactory Factory { get; set; }
        protected ObservableDictionary<int, DataItem> DataItemsDic { get; set; }
        public FillDataDic(DataItemsFactory factory, ObservableDictionary<int, DataItem> dataItemsDic)
        {
            Factory = factory;
            DataItemsDic = dataItemsDic;
        }
        protected override DataItem GetDataItem()
        {
            return Factory.GetEmptyDataItem();
        }
        protected override string GetCommandText()
        {
            return DBHelper.GetSelectCommandText(Factory.FirstTableFields, Factory.TableName);
        }
        protected override void Fill(DbDataReader reader, DataItem dataItem)
        {
            base.Fill(reader, dataItem);
            DataItemsDic.Add(dataItem.Id, dataItem);
        }
    }
}
