using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InterfaceToDataBase;

namespace InterfaceToClient
{
    public class DeviceEventsDictionary : DataItemControllersDictionary
    {
        public DeviceEventsDictionary()
        { }

        protected override DataItemControllersFactory GetFactory()
        {
            return new DeviceEventControllersFactory();
        }

        public IEnumerable<DataItemController> GetEventsByDeviceId(int Id)
        {
            return DataItemControllersDic.Values.Where(dataItemController => ((DeviceEventController)dataItemController).Device.Id == Id);
        }
    }
}
