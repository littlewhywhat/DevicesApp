using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace MiaMain
{
    public static class DBHelper
    {

        public static DataItem FillDataItem(SqlDataReader reader, DataItem dataItem)
        {
            Enumerable.Range(0, reader.FieldCount).ForEach(index =>
            {
                var fieldName = reader.GetName(index);
                var deviceProperty = dataItem.GetType().GetProperty(fieldName);
                deviceProperty.SetValue(dataItem, Convert.ChangeType(reader[fieldName], deviceProperty.PropertyType));
            });
            return dataItem;
        }

        public static void PerformDBAction(SqlConnection connection, DBAction action)
        {
            connection.Open();
            action.Act(connection);
            connection.Close();
        }

        public static string GetFillCommandText(List<string> tableFields, string tableName)
        {
            return String.Format("SELECT {0} FROM {1}", string.Join(",", tableFields), tableName);            
        }

        public static string GetFillCommandForItemText(List<string> tableFields, string tableName, int id)
        {
            return String.Format("{0} WHERE Id = {1}", GetFillCommandText(tableFields, tableName), id);
        }

        public static SqlCommand GetCommand(string commandText, SqlConnection connection )
        {
 	        return new SqlCommand(commandText, connection);
        }
    }
}
