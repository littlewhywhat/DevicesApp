using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InterfaceToDataBase;

namespace InterfaceToClient
{
    public static class FactoriesVault
    {
        public static Dictionary<string, DataItemControllersFactory> Dic = new Dictionary<string, DataItemControllersFactory>();
        public static DicChangesGetter ChangesGetter;
        private static void InitFactoriesVault()
        {
            Dic.Add(TableNames.Devices, new DeviceControllersFactory(new DevicesFactory()));
            Dic.Add(TableNames.DeviceTypes, new DeviceTypeControllersFactory(new DeviceTypesFactory()));
            Dic.Add(TableNames.DeviceEvents, new DeviceEventControllersFactory(new DeviceEventsFactory()));
            Dic.Add(TableNames.Companies, new CompanyControllersFactory(new CompaniesFactory()));
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
