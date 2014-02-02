using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Data.SqlClient;
using System.Data.Common;

namespace InterfaceToDataBase
{
    internal class DeleteDataItem : ChangeData
    {
        public DeleteDataItem(DataItem dataitem): base(dataitem, ActionType.DELETE)
        { }

        protected override string GetMainCommandText()
        {
            return String.Format("Delete from {0} where Id = '{1}' ", dataItem.Factory.TableName, dataItem.Id);
        }
    }
}
