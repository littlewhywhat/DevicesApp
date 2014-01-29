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
        public DeviceTypeControllersFactory()
        { }

        public override DataItemController GetController(DataItem dataItem)
        {
            return new DeviceTypeController((DeviceType)dataItem, this);
        }

        protected override Grid GetTabItemContent()
        {
            var dataGrid = new TabItemGrid();
            dataGrid.ContentGrid = new DeviceTypesInfoGrid();
            return dataGrid;
        }

        protected override DataItemsFactory GetFactory()
        {
            return new DeviceTypesFactory();
        }
    }
}
