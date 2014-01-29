using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
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

        protected override Grid GetTabItemContent()
        {
            var dataGrid = new TabItemGrid();
            dataGrid.ContentGrid = new DeviceEventGrid();
            return dataGrid;
        }

        protected override DataItemsFactory GetFactory()
        {
            return new DeviceEventsFactory();
        }

    }
}
