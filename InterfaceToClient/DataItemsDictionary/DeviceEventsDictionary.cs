﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataItemsLibrary;

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

        public IEnumerable<DeviceEventController> GetEventsByDeviceId(int Id)
        {
            return DataItemControllersDic.Values.Where(dataItemController => ((DeviceEventController)dataItemController).Device.Id == Id)
                .Select(dataItemController => (DeviceEventController)dataItemController);
        }
    }
}
