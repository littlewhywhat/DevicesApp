using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Data.Common;

namespace MiaMain
{
    public static class FactoriesVault
    {
        public static Dictionary<string, DataItemsFactory> FactoriesDic = new Dictionary<string, DataItemsFactory>();
        public static DicChangesGetter ChangesGetter;
        static FactoriesVault()
        {
            var devicesFactory = new DevicesFactory();
            FactoriesDic.Add(devicesFactory.TableName, devicesFactory);
            var companiesFactory = new CompaniesFactory();
            FactoriesDic.Add(companiesFactory.TableName, companiesFactory);
            var eventsFactory = new DeviceEventsFactory();
            FactoriesDic.Add(eventsFactory.TableName, eventsFactory);
            ChangesGetter = new DicChangesGetter();
        }
        public static void FillFactories()
        {
            UpdateClient.SetTimestamp(Connection.GetLogTimestamp());
            FactoriesDic.ForEach(pair => pair.Value.FillDic());
            UpdateClient.Control();
        }
    }
}
