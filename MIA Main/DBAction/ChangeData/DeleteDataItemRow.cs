using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Data.SqlClient;
using System.Data.Common;

namespace MiaMain
{
    public class DeleteDataItemRow : ChangeData
    {
        internal DeleteDataItemRow(DataItem dataitem): base(dataitem, ActionType.DELETE)
        { }

        protected override string GetMainCommandText()
        {
            return String.Format("Delete from {0} where Id = '{1}' ", dataItem.Factory.TableName, dataItem.Id);
        }
    }
}
