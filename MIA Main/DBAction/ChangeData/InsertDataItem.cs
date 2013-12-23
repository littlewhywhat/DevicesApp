using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Reflection;

namespace MiaMain
{
    public class InsertDataItem : ChangeData
    {
        public InsertDataItem(DataItemsFactory factory, DataItem dataItem) : base(factory, dataItem, ActionType.INSERT)
        {}
        public override void Act(SqlConnection connection)
        {
            new GetNewDataItemId(Factory, dataItem).Act(connection);
            base.Act(connection);
        }

        protected override string GetMainCommandText()
        {
            return String.Format("Insert into {0} (Id, {1}) Values ({2},{3})",Factory.TableName, 
                            String.Join(",", dataItem.GetPropertyValueDic().Select(pair => pair.Key)),
                            dataItem.Id,
                            String.Join(",", dataItem.GetPropertyValueDic().Select(pair => String.Format("'{0}'", pair.Value))));
        }
    }
}
