using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Data.Common;


namespace InterfaceToDataBase
{
    public class GetNewDataItemId : DBAction
    {
        DataItem dataItem;
        public GetNewDataItemId(DataItem DataItem)
        {
            dataItem = DataItem;
        }
        public object Act(DbConnection connection)
        {
            dataItem.Id = GetNewId(connection) + 1;
            return dataItem;
        }

        private int GetNewId(DbConnection connection)
        {
            var result = Connection.GetCommand(GetNewIdCommandText(), connection).ExecuteScalar();
            return result is DBNull? 0 : Convert.ToInt32(result);
        }

        private string GetNewIdCommandText()
        {
            return String.Format("Select MAX(Id) from {0}", dataItem.Factory.TableName);
        }
    }
}
