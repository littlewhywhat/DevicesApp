using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InterfaceToDataBase;

namespace InterfaceToClient
{
    public class DeviceTypesDictionary : DataItemsDictionary
    {
        public DeviceTypesDictionary(DeviceTypesFactory factory) : base(factory)
        { }
    }
}
