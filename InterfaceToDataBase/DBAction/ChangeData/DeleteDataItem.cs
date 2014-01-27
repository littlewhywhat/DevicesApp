using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Data.SqlClient;
using System.Data.Common;

namespace InterfaceToDataBase
{
    public class DeleteDataItem : ChangeData
    {
        internal DeleteDataItem(DataItem dataitem): base(dataitem, ActionType.DELETE)
        { }

        protected override string GetMainCommandText()
        {
            return String.Format("Delete from {0} where Id = '{1}' ", dataItem.Factory.TableName, dataItem.Id);
        }
    }
}
