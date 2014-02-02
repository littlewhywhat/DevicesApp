using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;

namespace InterfaceToDataBase
{
    internal class PerformTransactionOnList : TransactionData
    {
        List<TransactionData> Actions;
        public PerformTransactionOnList(List<TransactionData> actions)
        {
            Actions = actions;
        }

        public override object PerformTransaction(DbTransaction Transaction)
        {
            Actions.ForEach(action => action.PerformTransaction(Transaction));
            return null;
        }
    }
}
