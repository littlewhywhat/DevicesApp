using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using DataItemsLibrary;
using DBActionLibrary;

namespace DataItemControllersLibrary
{
    public static class UpdateClient
    {
        private static byte[] timestamp = new byte[8];
        private static DictionariesVault Vault;
        public static void SetVault(DictionariesVault vault)
        {
            Vault = vault;
        }

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

                    var controllerDic = Vault.GetDicByTableName(change.TableName);
                    switch (change.ActionType)
                    {
                        case "INSERT":
                            controllerDic.AddDataItem(change.ItemId);
                            break;
                        case "DELETE":
                            controllerDic.RemoveDataItem(change.ItemId);
                            break;
                        case "UPDATE":
                            controllerDic.UpdateDataItem(change.ItemId);
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
