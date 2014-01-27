using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InterfaceToDataBase;

namespace InterfaceToClient
{
    public class DeviceEventsDictionary : DataItemsDictionary
    {
        public DeviceEventsDictionary(DeviceEventsFactory factory) : base(factory)
        { }
    }
}
