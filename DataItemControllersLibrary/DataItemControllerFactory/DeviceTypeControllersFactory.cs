using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using DataItemsLibrary;

namespace DataItemControllersLibrary
{
    public class DeviceTypeControllersFactory : DataItemControllersFactory
    {
        public DeviceTypeControllersFactory(DictionariesVault vault) : base(vault)
        { }

        public override DataItemController GetController(DataItem dataItem)
        {
            return new DeviceTypeController((DeviceType)dataItem, this);
        }
        protected override DataItemsFactory GetFactory()
        {
            return new DeviceTypesFactory();
        }
    }
}
