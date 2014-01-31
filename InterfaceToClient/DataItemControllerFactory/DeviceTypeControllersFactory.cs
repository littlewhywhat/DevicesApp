using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using InterfaceToDataBase;

namespace InterfaceToClient
{
    public class DeviceTypeControllersFactory : DataItemControllersFactory
    {
        public DeviceTypeControllersFactory()
        { }

        public override DataItemController GetController(DataItem dataItem)
        {
            return new DeviceTypeController((DeviceType)dataItem, this);
        }

        protected override DataItemsInfoGrid GetDataItemsInfoGrid()
        {
            return new DeviceTypesInfoGrid();
        }

        protected override DataItemsFactory GetFactory()
        {
            return new DeviceTypesFactory();
        }

        internal override FrameworkElement GetPanel()
        {
            return new DeviceTypePanel();
        }
    }
}
