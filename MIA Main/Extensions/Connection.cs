using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;
using System.Transactions;

namespace MiaMain
{
    public static class Connection
    {
        readonly static string ConnectionString = "Data Source=" + "WHYWHAT-PC\\SQLEXPRESS" + "; Integrated Security = SSPI; Initial Catalog=" + "MiaDB";
        static Dictionary<DbConnection, DbTransaction> TransactionsDic = new Dictionary<DbConnection,DbTransaction>();
        public static DbConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }
        public static DbCommand GetCommand(string commandText)
        {
            return new SqlCommand(commandText, (SqlConnection)GetConnection());
        }

        public static DbTransaction GetTransaction(DbConnection connection)
        {
            if (!TransactionsDic.ContainsKey(connection))
                TransactionsDic.Add(connection, connection.BeginTransaction());
            return TransactionsDic[connection];
        }

        public static void TransactionCommit(DbTransaction transaction)
        {
            TransactionsDic.Remove(transaction.Connection);
            transaction.Commit();
        }

        public static void TransactionRollBack(DbTransaction transaction)
        {
            transaction.Rollback();
            TransactionsDic.Remove(transaction.Connection);
        }

        public static DbCommand GetCommand(string commandText, DbConnection connection)
        {
            var command = connection.CreateCommand();
            command.CommandText = commandText;
            if (TransactionsDic.ContainsKey(connection))
                command.Transaction = TransactionsDic[connection];
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
