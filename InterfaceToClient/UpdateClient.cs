using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using InterfaceToDataBase;

namespace InterfaceToClient
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
                    
                    var controllerFactory = FactoriesVault.Dic[change.TableName];
                    switch (change.ActionType)
                    {
                        case "INSERT":
                            controllerFactory.DataItemsDic.AddDataItem(change.ItemId);
                            break;
                        case "DELETE":
                            controllerFactory.DataItemsDic.RemoveDataItem(change.ItemId);
                            break;
                        case "UPDATE":
                            controllerFactory.DataItemsDic.UpdateDataItem(change.ItemId);
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
            workerThread.IsBackground = true;
            workerThread.Start();
        }
    }
}
