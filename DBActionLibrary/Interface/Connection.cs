using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;

using DataItemsLibrary;

namespace DBActionLibrary
{
    public static class Connection
    {
        internal static DbConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }
        internal static DbCommand GetCommand(string commandText, DbConnection connection)
        {
            var command = connection.CreateCommand();
            command.CommandText = commandText;
            return command;
        }
        internal static DbCommand GetCommand(string commandText, DbTransaction transaction)
        {
            var command = transaction.Connection.CreateCommand();
            command.CommandText = commandText;
            command.Transaction = transaction;
            return command;
        }

        public static string ConnectionString { get; set; }
        public static DbParameter GetTimestampParameter(string parameterName, object value)
        {
            return new SqlParameter(parameterName, SqlDbType.Timestamp) { Value = value };
        }
        public static byte[] GetLogTimestamp()
        {
            return (byte[])DBHelper.PerformDBAction(new GetTimestamp());
        }
        public static List<LogRow> GetLogRowList(byte[] timestamp)
        {
            return (List<LogRow>)DBHelper.PerformDBAction(new GetLoggingReader(timestamp));
        }
        public static bool CheckConnection()
        {
            try { using (var connection = GetConnection()) { connection.Open(); } return true; }
            catch (Exception) { return false; }
        }
        public static DbConnectionStringBuilder GetConnectionStringBuilder()
        {
            return new SqlConnectionStringBuilder();
        }

    }
}
