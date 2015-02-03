using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.IO;
using DBActionLibrary;

namespace MiaMain
{
    public static class ConnectionStringBuilder
    {
        static readonly string FilePath = "ConnectionString.txt";
        public static bool SetConnectionString()
        {
            if (!SetConnectionStringFromFile())
                return SetConnectionStringFromWindow(Connection.GetConnectionStringBuilder());
            return true;
        }

        private static bool SetConnectionStringFromWindow(DbConnectionStringBuilder builder)
        {
            var connectionWindow = new ConnectionWindow() { DataContext = builder };
            return ((bool)connectionWindow.ShowDialog());
        }

        private static bool SetConnectionStringFromFile()
        {
            Connection.ConnectionString = GetConnectionStringFromFile();
            return Connection.CheckConnection();
        }

        private static string GetConnectionStringFromFile()
        {
            try { return File.ReadAllText(FilePath); }
            catch (FileNotFoundException) { return null; }
        }

        internal static void SaveConnectionStringToFile(string connectionString)
        {
            File.WriteAllText(FilePath, connectionString);
        }
    }
}
