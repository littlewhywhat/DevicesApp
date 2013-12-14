using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace MIA_Main
{
    static class DBHelper
    {
        public static SqlConnection Connection { get; private set; }
        public static void SetConnection(SqlConnectionStringBuilder builder)
        {
            Connection = new SqlConnection(builder.ConnectionString);

        }
        private static void GetData()
        {
            string commandText = GetDeviceInfoCommand();
            
            SqlCommand command = new SqlCommand(commandText, Connection);
            var reader = command.ExecuteReader();
            for (var i = 0; i < reader.FieldCount; i++)
            {
                reader.GetValue(i);

            }
                
        }

        private static string GetDeviceInfoCommand()
        {
            return "SELECT * FROM Devices";
        }
    }
}
