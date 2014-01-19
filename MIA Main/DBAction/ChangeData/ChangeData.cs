using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Data.Common;

namespace MiaMain
{
    public abstract class ChangeData : TransactionData
    {
        protected DbTransaction Transaction;
        protected DataItem dataItem;
        ActionType ActionType;
        public ChangeData(DataItem dataItem, ActionType actionType)
        {
            ActionType = actionType;
            this.dataItem = dataItem;
        }

        private void ExecPreCommand(DbConnection connection)
        {
            Connection.GetCommand(DBHelper.GetPreActionText(), connection).ExecuteNonQuery();
        }

        protected virtual int ExecMainCommand(DbTransaction Transaction)
        {
            var command = Connection.GetCommand(GetMainCommandText(), Transaction.Connection);
            command.Transaction = Transaction;
            return command.ExecuteNonQuery();
        }

        private void ExecLogCommand(DbTransaction Transaction)
        {
            var command = Connection.GetCommand(DBHelper.GetLogCommandText(dataItem.Factory.TableName, dataItem.Id, ActionType), Transaction.Connection);
            command.Transaction = Transaction;
            command.ExecuteNonQuery();
        }

        protected abstract string GetMainCommandText();

        public override void PerformTransaction(DbTransaction Transaction)
        {
            var result = ExecMainCommand(Transaction);
            ExecLogCommand(Transaction);
            if (result == 0)
                throw new Exception();
        }
    }
}
