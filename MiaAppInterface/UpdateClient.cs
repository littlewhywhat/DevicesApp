using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Threading;
using MiaMain;

namespace MiaAppInterface
{
    public static class UpdateClient
    {
        private static byte[] timestamp = new byte[8];
        private static MainWindow mainWindow;
        public static void SetTimestamp(byte[] newTimestamp)
        {
            timestamp = newTimestamp;
        }
        public static void SetMainWindow(MainWindow MainWindow)
        {
            mainWindow = MainWindow;
        }
        private static void Do(SqlConnection connection)
        {
            var tempTimestamp = (byte[])DBHelper.PerformDBAction(connection, new GetTimestamp());
            if (!tempTimestamp.SequenceEqual(timestamp))
            {
                
                var InfoList = (List<LogRow>)DBHelper.PerformDBAction(connection, new GetLoggingReader(timestamp));
                InfoList.ForEach(item =>
                {
                    var factory = FactoriesVault.FactoriesDic[item.TableName];
                    switch (item.ActionType)
                    {
                        case "INSERT":
                            var dataItem = factory.GetDataItem();
                            dataItem.Id = item.ItemId;
                            DBHelper.PerformDBAction(new SqlConnection(connection.ConnectionString), new FillDataItem(dataItem, factory, factory.FirstTableFields));
                            factory.GetDataItemsDic().Add(dataItem.Id, dataItem);
                            break;
                        case "DELETE":
                            factory.GetDataItemsDic().Remove(item.ItemId);
                            break;
                        case "UPDATE":
                            var dataItemUpdate = factory.GetDataItem();
                            dataItemUpdate.Id = Convert.ToInt32(item.ItemId);
                            DBHelper.PerformDBAction(new SqlConnection(connection.ConnectionString), new FillDataItem(dataItemUpdate, factory, factory.FirstTableFields));
                            DBHelper.PerformDBAction(new SqlConnection(connection.ConnectionString), new FillDataItem(dataItemUpdate, factory, factory.OtherTableFields));
                            factory.GetDataItemsDic()[dataItemUpdate.Id] = dataItemUpdate;
                            var tabItem = mainWindow.DataItemsTabControl.GetTabItemByDataContext(dataItemUpdate.Id);
                            if (tabItem != null)
                                tabItem.Dispatcher.BeginInvoke(new ThreadStart(() => tabItem.DataContext = dataItemUpdate));
                            break;

                    }


                });
                //Console.WriteLine(String.Format("{0} {1} {2} {3}", reader["Id"], reader["TableName"], reader["ItemId"], reader["ActionType"]));
                
                
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
