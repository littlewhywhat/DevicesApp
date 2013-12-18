using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace MiaMain
{
    public class FillDataItem : FillData
    {
        private DataItem DataItem { get; set; }
        public FillDataItem(DataItem dataItem, DataItemsFactory factory)
            : base(factory)
        {
            DataItem = dataItem;
        }
        protected override string GetCommandText()
        {
            return DBHelper.GetFillCommandTextForItem(Factory.OtherTableFields, Factory.TableName, DataItem.Id);
        }
        protected override DataItem GetDataItem()
        {
            return DataItem;
        }
    }
}
