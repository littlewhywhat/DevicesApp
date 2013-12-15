using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace MIA_Main
{
    public class DBHelper
    {
        public static SqlConnection Connection { get; private set; }
        public DBHelper(string connectionString)
        {
            Connection = new SqlConnection(connectionString);
        }
        
        public Dictionary<int, Device> GetDevicesDictionary(List<string> tableFields)
        {
            var devicesDictionary = new Dictionary<int, Device>();
            using (var reader = new SqlCommand(GetDeviceInfoCommand(), Connection).ExecuteReader())
            {
                while (reader.Read())
                {
                    var device = new Device();
                    tableFields.ForEach( (fieldName) =>
                    {
                        var field = Convert.ToInt32(reader[fieldName]);
                        device.GetType().GetProperty(fieldName).SetValue(device, field);                        
                    });
                    devicesDictionary.Add(device.Id, device);
                }
            }
            return devicesDictionary;                
        }

        private string GetDeviceInfoCommand()
        {
            return "SELECT id FROM Devices";
        }
    }
}
