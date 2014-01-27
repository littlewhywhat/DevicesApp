using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InterfaceToDataBase;

namespace InterfaceToClient
{
    public class DeviceEventController : DataItemController
    {
        public DeviceEventController(DeviceEvent deviceEvent, DeviceEventControllersFactory factory ) : base(deviceEvent , factory)
        { }

        public int Device { get { return ((DeviceEvent)DataItem).DeviceId; } }

        public string Type { get { return ((DeviceEvent)DataItem).Type; } set { ((DeviceEvent)DataItem).Type = value; } }



        public override bool IsTheSameByType(DataItemController controller)
        {
            return controller is DeviceEventController;
        }
    }
}
