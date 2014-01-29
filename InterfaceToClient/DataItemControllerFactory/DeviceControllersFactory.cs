using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using InterfaceToDataBase;

namespace InterfaceToClient
{
    public class DeviceControllersFactory : DataItemControllersFactory
    {
        public DeviceControllersFactory()
        { }

        public override DataItemController GetController(DataItem dataItem)
        {
            return new DeviceController(dataItem, this);
        }

        protected override Grid GetTabItemContent()
        {
            return new DevicesGrid();
        }

        protected override DataItemsFactory GetFactory()
        {
            return new DevicesFactory();
        }

        public override DataItemsTabItem GetTabItem(DataItemController dataItemController)
        {
            var tabItem = base.GetTabItem(dataItemController);
            FactoriesVault.ChangesGetter.AddObserver(tabItem, new string[] { TableNames.DeviceTypes });
            return tabItem;
        }
    }
}
