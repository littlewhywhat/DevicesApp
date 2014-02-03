using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataItemControllersLibrary;
using System.Windows;
using System.Windows.Controls;

namespace InterfaceToClient
{
    public class ExtDeviceEventControllersFactory : DeviceEventControllersFactory, IExtDataItemControllersFactory
    {
        public ExtDeviceEventControllersFactory(DictionariesVault vault) : base(vault) { }

        public DataItemsInfoGrid GetDataItemsInfoGrid(DataItemController controller)
        {
            return new DeviceEventGrid();
        }

        public DataItemsTabItem GetTabItem(DataItemController controller)
        {
            return controller.GetTabItemDefault();
        }

        public FrameworkElement GetPanel(DataItemController controller)
        {
            throw new NotImplementedException();
        }

        public Grid GetTabItemContent(DataItemController controller)
        {
            return controller.GetTabItemContentDefault();
        }
    }
}
