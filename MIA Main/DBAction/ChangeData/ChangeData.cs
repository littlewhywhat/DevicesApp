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
        ActionType ActionType;
        public ChangeData(DataItemsFactory factory, DataItem dataItem, ActionType actionType)
        {
            ActionType = actionType;
            Factory = factory;
            this.dataItem = dataItem;
        }
        public virtual void Act(SqlConnection connection)
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

        protected virtual void ExecMainCommand(SqlConnection connection)
        {
            new SqlCommand(GetMainCommandText(), connection) { Transaction = Transaction }.ExecuteNonQuery();
        }

        private void ExecLogCommand(SqlConnection connection)
        {
            new SqlCommand(DBHelper.GetLogCommandText(Factory.TableName, dataItem.Id, ActionType), connection) { Transaction = Transaction }.ExecuteNonQuery();
        }

        protected abstract string GetMainCommandText();



    }
}
