using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InterfaceToDataBase;

namespace InterfaceToClient
{
    public class DeviceTypesDictionary : DataItemControllersDictionary
    {
        public DeviceTypesDictionary()
        { }


        protected override DataItemControllersFactory GetFactory()
        {
            return new DeviceTypeControllersFactory();
        }

        public IEnumerable<DataItemController> GetTypesWithoutMarker()
        {
            return DataItemControllersDic.Values.Where(dataItemController => !((DeviceTypeController)dataItemController).IsMarker);
        }
    }
}
