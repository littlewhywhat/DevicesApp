using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;

namespace MiaMain
{
    public class ConnectionWithTransaction : DbConnection
    {
        DbConnection connection;
        DbTransaction transaction;
        public ConnectionWithTransaction()
        {
            connection = Connection.GetConnection();
        }

        public DbTransaction GetCurrentDbTransaction()
        {
            return transaction;
        }
        protected override DbTransaction BeginDbTransaction(System.Data.IsolationLevel isolationLevel)
        {
            transaction = connection.BeginTransaction();
            return transaction;
        }
        

        public override void ChangeDatabase(string databaseName)
        {
            connection.ChangeDatabase(databaseName);
        }

        public override void Close()
        {
            connection.Close();
        }

        public override string ConnectionString
        {
            get
            {
                return connection.ConnectionString;
            }
            set
            {
                connection.ConnectionString = value;
            }
        }

        protected override DbCommand CreateDbCommand()
        {
            return connection.CreateCommand();
        }

        public override string DataSource
        {
            get { return connection.DataSource; }
        }

        public override string Database
        {
            get { return connection.Database; }
        }

        public override void Open()
        {
            connection.Open();
        }

        public override string ServerVersion
        {
            get { return connection.ServerVersion; }
        }

        public override System.Data.ConnectionState State
        {
            get { return connection.State; }
        }
    }
}
