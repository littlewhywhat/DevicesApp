using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;

namespace MiaMain
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
            return Convert.ToInt32(Connection.GetCommand(GetNewIdCommandText(), connection).ExecuteScalar());
        }

        private string GetNewIdCommandText()
        {
            return String.Format("Select MAX(Id) from {0}", dataItem.Factory.TableName);
        }
    }
}
