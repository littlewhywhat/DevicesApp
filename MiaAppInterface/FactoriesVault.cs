using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using MiaMain;

namespace MiaMain
{
    public static class FactoriesVault
    {
        public static Dictionary<string, DataItemsFactory> FactoriesDic = new Dictionary<string, DataItemsFactory>();
        static FactoriesVault()
        {
            var devicesFactory = new DevicesFactory();
            FactoriesDic.Add(devicesFactory.TableName, devicesFactory);
            var companiesFactory = new CompaniesFactory();
            FactoriesDic.Add(companiesFactory.TableName, companiesFactory);
        }
        public static void FillFactories(DbConnection connection)
        {
            FactoriesDic.ForEach(pair => DBHelper.PerformDBAction(connection, new FillDataDic(pair.Value)));
        }
    }
}
