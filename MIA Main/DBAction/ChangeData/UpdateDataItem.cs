using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace MiaMain
{
    public class UpdateDataItem : ChangeData
    {
        public UpdateDataItem(DataItemsFactory factory, DataItem dataItem) : base(factory, dataItem)
        { }

        protected override void ExecMainCommand(SqlConnection connection)
        {
            new SqlCommand(GetMainCommandText(), connection) { Transaction = Transaction }.ExecuteNonQuery();
        }


        protected override ActionType GetActionType()
        {
            return ActionType.UPDATE;
        }
        private string GetValues()
        {
            return String.Join(",", dataItem.GetType().GetProperties().Reverse().Skip(1).Select(deviceProperty =>
                {
                    return String.Format("{0}='{1}'", deviceProperty.Name, deviceProperty.GetValue(dataItem));
                }));
        }

        protected string GetMainCommandText()
        {
            return String.Format("UPDATE {0} Set {1} where Id = {2}", Factory.TableName, GetValues() , dataItem.Id);
        }
    }
}
