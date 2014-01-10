using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;

namespace MiaMain
{
    public abstract class ChangeData : DBAction
    {
        protected DbTransaction Transaction;
        protected DataItemsFactory Factory;
        protected DataItem dataItem;
        ActionType ActionType;
        public ChangeData(DataItemsFactory factory, DataItem dataItem, ActionType actionType)
        {
            ActionType = actionType;
            Factory = factory;
            this.dataItem = dataItem;
        }
        public virtual object Act(DbConnection connection)
        {
            ExecPreCommand(connection);
            Transaction = connection.BeginTransaction();
            try
            {
                ExecMainCommand(connection);
                ExecLogCommand(connection);
                Transaction.Commit();
            }
            catch (Exception)
            {
                Transaction.Rollback();
            }
            return null;
        }

        private void ExecPreCommand(DbConnection connection)
        {
            Connection.GetCommand(DBHelper.GetPreActionText(), connection).ExecuteNonQuery();
        }

        protected virtual void ExecMainCommand(DbConnection connection)
        {
            var command = Connection.GetCommand(GetMainCommandText(), connection);
            command.Transaction = Transaction;
            if (command.ExecuteNonQuery() == 0)
                throw new Exception();
        }

        private void ExecLogCommand(DbConnection connection)
        {
            var command = Connection.GetCommand(DBHelper.GetLogCommandText(Factory.TableName, dataItem.Id, ActionType), connection);
            command.Transaction = Transaction;
            command.ExecuteNonQuery();
        }

        protected abstract string GetMainCommandText();



    }
}
