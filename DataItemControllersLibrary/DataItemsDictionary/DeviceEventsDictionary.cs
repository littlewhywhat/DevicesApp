using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataItemsLibrary;

namespace DataItemControllersLibrary
{
    public class DeviceEventsDictionary : DataItemControllersDictionary
    {
        public DeviceEventsDictionary(DictionariesVault vault) : base(vault)
        { }

        public DeviceEventsDictionary(DeviceEventControllersFactory  factory) : base(factory)
        { }

        protected override DataItemControllersFactory GetFactory(DictionariesVault vault)
        {
            return new DeviceEventControllersFactory(vault);
        }

        internal IEnumerable<DeviceEventController> GetEventsByDeviceId(int Id)
        {
            return DataItemControllersDic.Values.Where(dataItemController => ((DeviceEventController)dataItemController).Device.Id == Id)
                .Select(dataItemController => (DeviceEventController)dataItemController);
        }
    }
}
