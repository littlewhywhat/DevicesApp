using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Data.Common;


namespace InterfaceToDataBase
{
    internal class GetNewDataItemId : TransactionData
    {
        DataItem dataItem;
        public GetNewDataItemId(DataItem DataItem)
        {
            dataItem = DataItem;
        }


        private int GetNewId(DbTransaction Transaction)
        {
            var result = Connection.GetCommand(GetNewIdCommandText(), Transaction).ExecuteScalar();
            return result is DBNull? 0 : Convert.ToInt32(result);
        }

        

        private string GetNewIdCommandText()
        {
            return String.Format("Select MAX(Id) from {0}", dataItem.Factory.TableName);
        }

        public override object PerformTransaction(DbTransaction Transaction)
        {
            dataItem.Id = GetNewId(Transaction) + 1;
            return dataItem;
        }
    }
}
