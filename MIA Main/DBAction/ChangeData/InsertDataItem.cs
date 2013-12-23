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
        public InsertDataItem(DataItemsFactory factory, DataItem dataItem) : base(factory, dataItem)
        {}
        
        protected override ActionType GetActionType()
        {
            return ActionType.INSERT;
        }

        protected override void ExecMainCommand(System.Data.SqlClient.SqlConnection connection)
        {
            dataItem.Id = GetNewId(connection) + 1;
            new SqlCommand(GetMainCommandText(), connection) { Transaction = Transaction }.ExecuteNonQuery();
        }

        private int GetNewId(SqlConnection connection)
        {
            return Convert.ToInt32(new SqlCommand(GetIdCommandText(), connection) { Transaction = Transaction }.ExecuteScalar());
        }

        private string GetIdCommandText()
        {
            return String.Format("Select MAX(Id) from {0}", Factory.TableName);
        }

        private string GetPropertyNames(IEnumerable<PropertyInfo> properties)
        {
            return String.Join(",", properties.Select(deviceProperty => deviceProperty.Name));
        }
        private string GetPropertyValues(IEnumerable<PropertyInfo> properties)
        {
            return String.Join(",", properties.Select(deviceProperty =>
                {
                    return String.Format("'{0}'", deviceProperty.GetValue(dataItem));
                }));
        }

        private string GetMainCommandText()
        {
            var properties = dataItem.GetType().GetProperties();
            var str = String.Format("Insert into {0} ({1}) Values ({2})",Factory.TableName, GetPropertyNames(properties), GetPropertyValues(properties));
            return str;
        }
    }
}
