using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using DataItemControllersLibrary;

namespace InterfaceToClient
{
    public class ExtDeviceTypeControllersFactory : DeviceTypeControllersFactory, IExtDataItemControllersFactory
    {
        public ExtDeviceTypeControllersFactory(DictionariesVault vault) : base(vault) { }

        public DataItemsInfoGrid GetDataItemsInfoGrid(DataItemController controller)
        {
            return new DeviceTypesInfoGrid();
        }

        public DataItemsTabItem GetTabItem(DataItemController controller)
        {
            return controller.GetTabItemDefault();
        }

        public FrameworkElement GetPanel(DataItemController controller)
        {
            return new DeviceTypePanel();
        }

        public Grid GetTabItemContent(DataItemController controller)
        {
            return controller.GetTabItemContentDefault();
        }
    }
}
