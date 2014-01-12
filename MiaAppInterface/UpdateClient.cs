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
            var tempTimestamp = Connection.GetLogTimestamp();
            if (!tempTimestamp.SequenceEqual(timestamp))
            {
                var LogRowList = Connection.GetLogRowList(timestamp);
                LogRowList.ForEach(change =>
                {
                    var factory = FactoriesVault.FactoriesDic[change.TableName];
                    switch (change.ActionType)
                    {
                        case "INSERT":
                            var dataItem = factory.GetFilledDataItem(change.ItemId);
                            factory.GetDataItemsDic().Add(dataItem.Id, dataItem);
                            break;
                        case "DELETE":
                            factory.GetDataItemsDic().Remove(change.ItemId);
                            break;
                        case "UPDATE":
                            var dataItemUpdate = factory.GetFilledDataItem(change.ItemId);
                            dataItemUpdate.Fill(factory.OtherTableFields);
                            factory.GetDataItemsDic()[dataItemUpdate.Id] = dataItemUpdate;
                            var tabItem = mainWindow.DataItemsTabControl.GetTabItemByDataContext(dataItemUpdate.Id);
                            if (tabItem != null)
                                tabItem.Dispatcher.BeginInvoke(new ThreadStart(() => tabItem.DataContext = dataItemUpdate));
                            break;
                    }
                });
                SetTimestamp(tempTimestamp);
            }
        }
        public static void Control(string connectionString)
        {
            using(var connection = new SqlConnection(connectionString))
            {
                while (true)
                {
                    Do(connection);
                    Thread.Sleep(1000);
                }
            }
        }
    }
}
