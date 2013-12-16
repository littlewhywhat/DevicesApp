using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace MiaMain
{
    public abstract class DBHelper
    {
        public SqlConnection Connection { get; private set; }
        public string TableName { get; set; }

        public DBHelper(string connectionString)
        {
            Connection = new SqlConnection(connectionString);
        }
        
        public Dictionary<int, DataItem> GetDataItemsDictionary(List<string> tableFields)
        {
            var devicesDictionary = new Dictionary<int, DataItem>();
            using (Connection)
            {
                Connection.Open();
                using (var reader = GetDataReader(tableFields))
                {
                    while (reader.Read())
                    {
                        var dataItem = FillDataItem(reader, GetDataItem() );
                        devicesDictionary.Add(dataItem.Id, dataItem);   
                    }
                }
            }
            return devicesDictionary;                
        }
        protected abstract DataItem GetDataItem();

        public DataItem FillDataItem(SqlDataReader reader, DataItem dataItem)
        {
            Enumerable.Range(0, reader.FieldCount).ForEach(index =>
            {
                var fieldName = reader.GetName(index);
                var deviceProperty = dataItem.GetType().GetProperty(fieldName);
                deviceProperty.SetValue(dataItem, Convert.ChangeType(reader[fieldName], deviceProperty.PropertyType));
            });
            return dataItem;
        }

        private string GetDataCommandText(List<string> tableFields)
        {
            return String.Format("SELECT {0} FROM {1}", string.Join(",", tableFields), TableName);            
        }
        private SqlDataReader GetDataReader(List<string> tableFields)
        {
            return new SqlCommand(GetDataCommandText(tableFields), Connection).ExecuteReader();
        }

    }
}
