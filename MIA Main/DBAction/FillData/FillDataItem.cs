using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace MiaMain
{
    public class FillDataItem : GetData
    {
        private DataItem DataItem { get; set; }
        private List<string> TableFields { get; set; }
        public FillDataItem(DataItem dataItem, DataItemsFactory factory, List<string> tableFields)
            : base(factory)
        {
            DataItem = dataItem;
            TableFields = tableFields;
        }
        protected override string GetCommandText()
        {
            return DBHelper.GetSelectCommandText(TableFields, Factory.TableName, DataItem.Id);
        }
        protected override DataItem GetDataItem()
        {
            return DataItem;
        }

        
    }
}
