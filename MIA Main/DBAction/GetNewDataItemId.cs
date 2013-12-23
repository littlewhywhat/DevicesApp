using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace MiaMain
{
    class GetNewDataItemId : DBAction
    {
        DataItemsFactory Factory;
        DataItem DataItem;
        public GetNewDataItemId(DataItemsFactory factory, DataItem dataItem)
        {
            Factory = factory;
            DataItem = dataItem;
        }
        public object Act(SqlConnection connection)
        {
            DataItem.Id = GetNewId(connection) + 1;
            return DataItem;
        }

        private int GetNewId(SqlConnection connection)
        {
            return Convert.ToInt32(new SqlCommand(GetNewIdCommandText(), connection).ExecuteScalar());
        }

        private string GetNewIdCommandText()
        {
            return String.Format("Select MAX(Id) from {0}", Factory.TableName);
        }
    }
}
