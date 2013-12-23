using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace MiaMain
{
    public class DeleteDataItem : ChangeData
    {
        public DeleteDataItem(DataItemsFactory factory, DataItem dataitem): base(factory, dataitem, ActionType.DELETE)
        { }

        protected override string GetMainCommandText()
        {
            return String.Format("Delete from {0} where Id = '{1}'", Factory.TableName, dataItem.Id);
        }
    }
}
