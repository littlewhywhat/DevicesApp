using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using InterfaceToDataBase;

namespace InterfaceToClient
{
    public class DeviceTypeControllersFactory : DataItemControllersFactory
    {
        public DeviceTypeControllersFactory(DeviceTypesFactory factory) : base(factory)
        { }

        public override DataItemController GetController(DataItem dataItem)
        {
            return new DeviceTypeController((DeviceType)dataItem, this);
        }

        protected override DataItemsDictionary GetDataItemsDictionary(DataItemsFactory factory)
        {
            return new DeviceTypesDictionary((DeviceTypesFactory)factory);
        }

        protected override Grid GetTabItemContent()
        {
            var dataGrid = new TabItemGrid();
            dataGrid.ContentGrid = new DeviceTypesInfoGrid();
            return dataGrid;
        }
    }
}
