using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace MiaMain
{
    public abstract class ChangeData : DBAction
    {
        protected SqlTransaction Transaction;
        protected DataItemsFactory Factory;
        protected DataItem dataItem;
        public ChangeData(DataItemsFactory factory, DataItem dataItem)
        {
            Factory = factory;
            this.dataItem = dataItem;
        }
        protected abstract ActionType GetActionType();
        public void Act(SqlConnection connection)
        {
            ExecPreCommand(connection);
            Transaction = connection.BeginTransaction();
            try
            {
                ExecMainCommand(connection);
                ExecLogCommand(connection);
                Transaction.Commit();
            }
            catch (SqlException)
            {
                Transaction.Rollback();
            }
        }

        private void ExecPreCommand(SqlConnection connection)
        {
            new SqlCommand(DBHelper.GetPreActionText(), connection).ExecuteNonQuery();
        }

        protected abstract void ExecMainCommand(SqlConnection connection);

        protected void ExecLogCommand(SqlConnection connection)
        {
            new SqlCommand(DBHelper.GetLogCommandText(Factory.TableName, dataItem.Id, GetActionType()), connection) { Transaction = Transaction }.ExecuteNonQuery();
        }

    }
}
