using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Data.SqlClient;

namespace MiaMain
{
    public class FillDataItem : GetData
    {
        private DataItem DataItem { get; set; }
        private List<string> TableFields { get; set; }
        public FillDataItem(DataItem dataItem, List<string> tableFields)
        {
            DataItem = dataItem;
            TableFields = tableFields;
        }
        protected override string GetCommandText()
        {
            return DBHelper.GetSelectCommandText(TableFields, DataItem.Factory.TableName, DataItem.Id);
        }
        protected override DataItem GetDataItem()
        {
            return DataItem;
        }

        
    }
}
