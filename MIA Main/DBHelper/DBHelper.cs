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

        public static object PerformDBAction(SqlConnection connection, DBAction action)
        {
            connection.Open();
            var result = action.Act(connection);
            connection.Close();
            return result;
        }

        public static SqlCommand GetCommand(string commandText, SqlConnection connection )
        {
 	        return new SqlCommand(commandText, connection);
        }
        
        public static string GetSelectCommandText(List<string> tableFields, string tableName, int id = 0)
        {
            var commandText = String.Format("SELECT {0} FROM {1}", string.Join(",", tableFields), tableName);
            return id != 0 ? String.Format("{0} WHERE id = {1}", commandText, id) : commandText;
        }
        
        public static string GetLogCommandText(string tableName, int id, ActionType actionType)
        {
            return String.Format("INSERT INTO Logging (TableName, ItemId, ActionType) VALUES ('{0}', '{1}', '{2}')", tableName, id, actionType.ToString());
        }
        
        public static string GetPreActionText()
        {
            return "set ansi_nulls off set xact_abort on";
        }
    }
}
