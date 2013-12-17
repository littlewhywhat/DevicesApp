using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace MiaMain
{
    class FillDataItem : DBAction
    {
        DataItem DataItem { get; set; }
        string CommandText { get; set; }
        public FillDataItem(DataItem dataItem, string commandText)
        {
            DataItem = dataItem;
            CommandText = commandText;
        }
        public void Act(SqlConnection connection)
        {
            var reader = DBHelper.GetCommand(CommandText, connection).ExecuteReader();
            reader.Read();
            DBHelper.FillDataItem(reader, DataItem);
        }
    }
}
