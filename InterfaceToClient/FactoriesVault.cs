using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InterfaceToDataBase;

namespace InterfaceToClient
{
    public static class FactoriesVault
    {
        public static Dictionary<string, DataItemControllersDictionary> Dic = new Dictionary<string, DataItemControllersDictionary>();
        public static DicChangesGetter ChangesGetter;
        private static void InitFactoriesVault()
        {
            Dic.Add(TableNames.Devices, new DevicesDictionary());
            Dic.Add(TableNames.DeviceTypes, new DeviceTypesDictionary());
            Dic.Add(TableNames.DeviceEvents, new DeviceEventsDictionary());
            Dic.Add(TableNames.Companies, new CompaniesDictionary());
            ChangesGetter = new DicChangesGetter();
        }
        public static void FillFactories()
        {
            UpdateClient.SetTimestamp(Connection.GetLogTimestamp());
            InitFactoriesVault();
            UpdateClient.Control();
        }
    }
}
