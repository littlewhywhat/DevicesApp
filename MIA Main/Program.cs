using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Threading;

namespace MiaMain
{
    static class Program
    {
        public static void Main()
        {
            using (var connection = new SqlConnection("Data Source=" + "WHYWHAT-PC\\SQLEXPRESS" + "; Integrated Security = SSPI; Initial Catalog=" + "MiaDB"))
            {
                //var devicesFactory = new DevicesFactory();
                //DBHelper.PerformDBAction(connection, new FillDataDic(devicesFactory));
                //var devicesDic = devicesFactory.GetDataItemsDic();
                //Device device = (Device)devicesDic[9];
                //device.Info = "Device9";
                //device.CompanyId = 1;
                //DBHelper.PerformDBAction(connection, new UpdateDataItem(devicesFactory, device));


                
            };
        }
    }
}
