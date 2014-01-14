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
        private static void Do()
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
                            factory.GetDataItemsDic()[dataItemUpdate.Id] = dataItemUpdate;
                            break;
                    }
                });
                SetTimestamp(tempTimestamp);
            }
        }
        public static void Control()
        {
            var workerThread = new Thread(() =>
            {
                while(true)
                {
                    Do();
                    Thread.Sleep(1000);
                }
            }
            );
            workerThread.Start();
        }
    }
}
