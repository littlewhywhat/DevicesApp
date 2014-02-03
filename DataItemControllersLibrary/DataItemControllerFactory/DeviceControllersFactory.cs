using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using DataItemsLibrary;

namespace DataItemControllersLibrary
{
    public class DeviceControllersFactory : DataItemControllersFactory
    {
        public DeviceControllersFactory(DictionariesVault vault) : base(vault)
        { }

        public override DataItemController GetController(DataItem dataItem)
        {
            return new DeviceController(dataItem, this);
        }
        protected override DataItemsFactory GetFactory()
        {
            return new DevicesFactory();
        }
    }
}
