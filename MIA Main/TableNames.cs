using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiaMain
{
    public static class TableNames 
    {
        enum tableNames : byte
        {
            Devices = 0,
            DeviceEvents = 1,
            DeviceTypes = 2,
            Companies = 3,
        }

        public static string Devices { get { return tableNames.Devices.ToString(); } }
        public static string DeviceEvents { get { return tableNames.DeviceEvents.ToString(); } }
        public static string DeviceTypes { get { return tableNames.DeviceTypes.ToString(); } }
        public static string Companies { get { return tableNames.Companies.ToString(); } }


    }
}
