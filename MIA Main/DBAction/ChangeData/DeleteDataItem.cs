using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;

namespace MiaMain
{
    public class DeleteDataItem : TransactionData
    {
        DataItem DataItem;
        public DeleteDataItem(DataItem dataItem)
        {
            DataItem = dataItem;
        }
        public override void PerformTransaction(DbTransaction Transaction)
        {
            DataItem.Factory.DeleteTransaction(DataItem, Transaction);
        }
    }
}
