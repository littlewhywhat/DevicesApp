using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using InterfaceToDataBase;

namespace InterfaceToClient
{
    public class DeviceEventControllersFactory : DataItemControllersFactory
    {
        public DeviceEventControllersFactory()
        { }

        public override DataItemController GetController(DataItem dataItem)
        {
            return new DeviceEventController((DeviceEvent)dataItem, this);
        }

        protected override DataItemsInfoGrid GetDataItemsInfoGrid()
        {
            return new DeviceEventGrid();
        }

        protected override DataItemsFactory GetFactory()
        {
            return new DeviceEventsFactory();
        }


        internal override FrameworkElement GetPanel()
        {
            throw new NotImplementedException();
        }
    }
}
