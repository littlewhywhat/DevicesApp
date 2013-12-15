using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace MIA_Main
{
    public class DBHelper
    {
        public SqlConnection Connection { get; private set; }
        public DBHelper(string connectionString)
        {
            Connection = new SqlConnection(connectionString);
        }
        
        internal Dictionary<int, Device> GetDevicesDictionary(List<string> tableFields)
        {
            var devicesDictionary = new Dictionary<int, Device>();
            using (Connection)
            {
                Connection.Open();
                
                using (var reader = GetDeviceInfoCommand(tableFields).ExecuteReader())
                {
                    
                    while (reader.Read())
                    {
                        var device = new Device();
                        tableFields.ForEach((fieldName) =>
                        {
                            var deviceProperty = device.GetType().GetProperty(fieldName);
                            deviceProperty.SetValue(device, Convert.ChangeType(reader[fieldName], deviceProperty.PropertyType));
                        });
                        devicesDictionary.Add(device.Id, device);
                        
                    }
                }
            }
            return devicesDictionary;                
        }

        private SqlCommand GetDeviceInfoCommand(List<string> tableFields)
        {
            SqlCommand command = new SqlCommand("SELECT Id, Info FROM Devices", Connection);
            
            var param = new SqlParameter();
            param.ParameterName = "Id";
            
            param.Size = 50;
            
            param.Direction = ParameterDirection.Output;
            var param1 = new SqlParameter();
            param1.ParameterName = "Info";
            
            param1.Size = 50;
            param1.Direction = ParameterDirection.Output;
            command.Parameters.Add(param);
            command.Parameters.Add(param1);
            return command;
        }
    }
}
