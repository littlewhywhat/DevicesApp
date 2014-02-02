using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Data.Common;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using DataItemsLibrary;

namespace DBActionLibrary
{
    internal class FillDataDic : GetData
    {
        protected DataItemsFactory Factory { get; set; }
        protected IDataItemDic DataItemsDic { get; set; }
        public FillDataDic(DataItemsFactory factory, IDataItemDic dataItemsDic)
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
            DataItemsDic.AddDataItem(dataItem);
        }
    }
}
