using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using DataItemControllersLibrary;
using DataItemsLibrary;

namespace InterfaceToClient
{
    public class ExtDeviceControllersFactory : DeviceControllersFactory, IExtDataItemControllersFactory
    {
        public ExtDeviceControllersFactory(DictionariesVault vault) : base(vault) { }

        public DataItemsInfoGrid GetDataItemsInfoGrid(DataItemController controller)
        {
            return new DevicesInfoGrid();
        }

        public DataItemsTabItem GetTabItem(DataItemController controller)
        {
            var tabItem = controller.GetTabItemDefault();
            InterfaceToClient.Vault.ChangesGetter.AddObserver(tabItem, new string[] { TableNames.DeviceTypes });
            return tabItem;
        }

        public FrameworkElement GetPanel(DataItemController controller)
        {
            return new DevicePanel();
        }

        public Grid GetTabItemContent(DataItemController controller)
        {
            return new DevicesGrid();
        }
    }
}
