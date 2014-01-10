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
        DataItemsFactory Factory;
        DataItem DataItem;
        public GetNewDataItemId(DataItemsFactory factory, DataItem dataItem)
        {
            Factory = factory;
            DataItem = dataItem;
        }
        public object Act(DbConnection connection)
        {
            DataItem.Id = GetNewId(connection) + 1;
            return DataItem;
        }

        private int GetNewId(DbConnection connection)
        {
            return Convert.ToInt32(Connection.GetCommand(GetNewIdCommandText(), connection).ExecuteScalar());
        }

        private string GetNewIdCommandText()
        {
            return String.Format("Select MAX(Id) from {0}", Factory.TableName);
        }
    }
}
