using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;

namespace MiaMain
{
    public static class Connection
    {
        readonly static string ConnectionString = "Data Source=" + "WHYWHAT-PC\\SQLEXPRESS" + "; Integrated Security = SSPI; Initial Catalog=" + "MiaDB";
        public static DbConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }
        public static DbCommand GetCommand(string commandText)
        {
            return new SqlCommand(commandText, (SqlConnection)GetConnection());
        }
        public static DbCommand GetCommand(string commandText, DbConnection connection)
        {
            return new SqlCommand(commandText, (SqlConnection)connection);
        }
        public static DbParameter GetTimestampParameter(string parameterName, object value)
        {
            return new SqlParameter(parameterName, SqlDbType.Timestamp) { Value = value };
        }
    }
}
