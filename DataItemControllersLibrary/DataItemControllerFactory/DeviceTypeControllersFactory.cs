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
        protected override DataItemsFactory GetFactory()
        {
            return new DeviceTypesFactory();
        }

        internal override DataItemController GetController(DataItem dataItem)
        {
            return new DeviceTypeController((DeviceType)dataItem, this);
        }
    }
}
