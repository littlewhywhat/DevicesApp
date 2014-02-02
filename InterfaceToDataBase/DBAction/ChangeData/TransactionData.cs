using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;

namespace InterfaceToDataBase
{
    internal abstract class TransactionData : DBAction
    {

        public abstract object PerformTransaction(DbTransaction Transaction);
        public object Act(DbConnection connection)
        {
            Connection.GetCommand(DBHelper.GetPreActionText(), connection).ExecuteNonQuery();
            var Transaction = connection.BeginTransaction();
            object result = null;
            try
            {
                result = PerformTransaction(Transaction);
                Transaction.Commit();
                return result;
            }
            catch (Exception e)
            {
                Transaction.Rollback();
                throw new DBActionException(e);
            }
            
        }
    }
}
