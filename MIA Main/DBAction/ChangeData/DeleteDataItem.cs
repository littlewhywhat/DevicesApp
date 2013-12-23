using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace MiaMain
{
    public class DeleteDataItem : ChangeData
    {
        public DeleteDataItem(DataItemsFactory factory, DataItem dataitem): base(factory, dataitem)
        { }
        protected override ActionType GetActionType()
        {
            return ActionType.DELETE;
        }

        protected override void ExecMainCommand(System.Data.SqlClient.SqlConnection connection)
        {
            new SqlCommand(GetMainCommandText(), connection) { Transaction = Transaction }.ExecuteNonQuery();
        }

        private string GetMainCommandText()
        {
            return String.Format("Delete from {0} where Id = '{1}'", Factory.TableName, dataItem.Id);
        }
    }
}
