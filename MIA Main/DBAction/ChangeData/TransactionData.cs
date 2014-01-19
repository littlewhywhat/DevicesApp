using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;

namespace MiaMain
{
    public abstract class TransactionData : DBAction
    {

        public abstract void PerformTransaction(DbTransaction Transaction);
        public object Act(DbConnection connection)
        {
            Connection.GetCommand(DBHelper.GetPreActionText(), connection).ExecuteNonQuery();
            var Transaction = Connection.GetTransaction(connection);
            try
            {
                PerformTransaction(Transaction);
                Connection.TransactionCommit(Transaction);
            }
            catch (Exception)
            {
                Connection.TransactionRollBack(Transaction);
            }
            return null;
        }
    }
}
