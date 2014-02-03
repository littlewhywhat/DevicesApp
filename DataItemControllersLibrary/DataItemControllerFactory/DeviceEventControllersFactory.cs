using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using DataItemsLibrary;

namespace DataItemControllersLibrary
{
    public class DeviceEventControllersFactory : DataItemControllersFactory
    {
        public DeviceEventControllersFactory(DictionariesVault vault) : base(vault)
        { }

        public override DataItemController GetController(DataItem dataItem)
        {
            return new DeviceEventController((DeviceEvent)dataItem, this);
        }
        protected override DataItemsFactory GetFactory()
        {
            return new DeviceEventsFactory();
        }
    }
}
