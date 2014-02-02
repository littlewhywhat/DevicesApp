using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;


namespace InterfaceToDataBase
{
    public static class Connection
    {
        public static string ConnectionString { get; set; }
        
        static Dictionary<DbConnection, DbTransaction> TransactionsDic = new Dictionary<DbConnection,DbTransaction>();

        public static DbConnectionStringBuilder GetConnectionStringBuilder()
        {
            return new SqlConnectionStringBuilder();
        }

        public static DbConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }

        public static bool CheckConnection()
        {
            try { using (var connection = GetConnection()) { connection.Open(); } return true; }
            catch (Exception) { return false; }           
        }

        public static DbCommand GetCommand(string commandText)
        {
            return new SqlCommand(commandText, (SqlConnection)GetConnection());
        }

        public static DbCommand GetCommand(string commandText, DbConnection connection)
        {
            var command = connection.CreateCommand();
            command.CommandText = commandText;
            return command;
        }
        public static DbCommand GetCommand(string commandText, DbTransaction transaction)
        {
            var command = transaction.Connection.CreateCommand();
            command.CommandText = commandText;
            command.Transaction = transaction;
            return command;
        }
        public static DbParameter GetTimestampParameter(string parameterName, object value)
        {
            return new SqlParameter(parameterName, SqlDbType.Timestamp) { Value = value };
        }
        public static byte[] GetLogTimestamp()
        {
            return (byte[])DBHelper.PerformDBAction(GetConnection(), new GetTimestamp());
        }
        public static List<LogRow> GetLogRowList(byte[] timestamp)
        {
            return (List<LogRow>)DBHelper.PerformDBAction(GetConnection(), new GetLoggingReader(timestamp));
        }

    }
}
