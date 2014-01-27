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
        public DeviceEventControllersFactory(DeviceEventsFactory factory) : base(factory)
        { }

        public override DataItemController GetController(DataItem dataItem)
        {
            return new DeviceEventController((DeviceEvent)dataItem, this);
        }

        protected override DataItemsDictionary GetDataItemsDictionary(DataItemsFactory factory)
        {
            return new DeviceEventsDictionary((DeviceEventsFactory)factory);
        }

        protected override Grid GetTabItemContent()
        {
            var dataGrid = new TabItemGrid();
            dataGrid.ContentGrid = new DeviceEventGrid();
            return dataGrid;
        }
    }
}
