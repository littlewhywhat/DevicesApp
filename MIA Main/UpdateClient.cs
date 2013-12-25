using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Threading;

namespace MiaMain
{
    public static class UpdateClient
    {
        private static byte[] timestamp = new byte[8];
        public static void SetTimestamp(byte[] newTimestamp)
        {
            timestamp = newTimestamp;
        }
        private static void Do(SqlConnection connection)
        {
            var tempTimestamp = (byte[])DBHelper.PerformDBAction(connection, new GetTimestamp());
            if (!tempTimestamp.SequenceEqual(timestamp))
            {
                connection.Open();
                var reader = (SqlDataReader)new GetLoggingReader(timestamp).Act(connection);
                while (reader.Read())
                {
                    var factory = FactoriesVault.FactoriesDic[reader["TableName"].ToString()];
                    switch(reader["ActionType"].ToString())
                    {
                        case "INSERT" :                           
                            var dataItem = factory.GetDataItem();
                            dataItem.Id = Convert.ToInt32(reader["ItemId"]);
                            new FillDataItem(dataItem, factory);
                            factory.GetDataItemsDic().Add(dataItem.Id, dataItem);
                            break;
                        case "DELETE" :
                            factory.GetDataItemsDic().Remove(Convert.ToInt32(reader["ItemId"]));
                            break;
                        case "UPDATE" :
                            var dataItemUpdate = factory.GetDataItem();
                            dataItemUpdate.Id = Convert.ToInt32(reader["ItemId"]);
                            new FillDataItem(dataItemUpdate, factory);
                            factory.GetDataItemsDic()[dataItemUpdate.Id] = dataItemUpdate;
                            break;
                            
                    }
                        
                    
                }
                //Console.WriteLine(String.Format("{0} {1} {2} {3}", reader["Id"], reader["TableName"], reader["ItemId"], reader["ActionType"]));
                connection.Close();
                
                timestamp = tempTimestamp;
            }
        }
        public static void Control(string connectionString)
        {
            using(var connection = new SqlConnection(connectionString))
            {
                while (true)
                {
                    Do(connection);
                    Thread.Sleep(5000);
                }
            }
        }
    }
}
