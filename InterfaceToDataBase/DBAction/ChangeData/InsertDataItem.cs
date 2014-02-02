using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Data.Common;
using System.Reflection;

namespace InterfaceToDataBase
{
    internal class InsertDataItem : ChangeData
    {
        public InsertDataItem(DataItem dataItem)
            : base(dataItem, ActionType.INSERT)
        {}
        protected override int ExecMainCommand(DbTransaction transaction)
        {
            new GetNewDataItemId(dataItem).PerformTransaction(transaction);
            return base.ExecMainCommand(transaction);
        }

        protected override string GetMainCommandText()
        {
            return String.Format("Insert into {0} (Id, {1}) Values ({2},{3})", dataItem.Factory.TableName, 
                            String.Join(",", dataItem.GetPropertyValueDic().Select(pair => pair.Key).ToArray()),
                            dataItem.Id,
                            String.Join(",", dataItem.GetPropertyValueDic().Select(pair => String.Format("'{0}'", pair.Value)).ToArray()));
        }
    }
}
