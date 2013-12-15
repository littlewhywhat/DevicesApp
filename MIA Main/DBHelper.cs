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
                using (var reader = GetDataReader(tableFields))
                {
                    while (reader.Read())
                    {
                        var device = FillDevice(reader, new Device());
                        devicesDictionary.Add(device.Id, device);   
                    }
                }
            }
            return devicesDictionary;                
        }

        public Device FillDevice(SqlDataReader reader, Device device)
        {
            Enumerable.Range(0, reader.FieldCount).ForEach(index =>
            {
                var fieldName = reader.GetName(index);
                var deviceProperty = device.GetType().GetProperty(fieldName);
                deviceProperty.SetValue(device, Convert.ChangeType(reader[fieldName], deviceProperty.PropertyType));
            });
            return device;
        }

        private string GetDataCommandText(List<string> tableFields)
        {
            return String.Format("SELECT {0} FROM Devices", string.Join(",", tableFields));            
        }
        private SqlDataReader GetDataReader(List<string> tableFields)
        {
            return new SqlCommand(GetDataCommandText(tableFields), Connection).ExecuteReader();
        }
    }
}
