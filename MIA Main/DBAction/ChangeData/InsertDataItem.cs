using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Reflection;

namespace MiaMain
{
    public class InsertDataItem : ChangeData
    {
        public InsertDataItem(DataItem dataItem) : base(dataItem, ActionType.INSERT)
        {}
        public override object Act(DbConnection connection)
        {
            new GetNewDataItemId(dataItem).Act(connection);
            return base.Act(connection);
        }

        protected override string GetMainCommandText()
        {
            return String.Format("Insert into {0} (Id, {1}) Values ({2},{3})", dataItem.Factory.TableName, 
                            String.Join(",", dataItem.GetPropertyValueDic().Select(pair => pair.Key)),
                            dataItem.Id,
                            String.Join(",", dataItem.GetPropertyValueDic().Select(pair => String.Format("'{0}'", pair.Value))));
        }
    }
}
