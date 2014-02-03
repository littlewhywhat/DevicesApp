using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using DataItemControllersLibrary;
namespace InterfaceToClient
{
    public interface IExtDataItemControllersFactory
    {
        DataItemsInfoGrid GetDataItemsInfoGrid(DataItemController controller);
        DataItemsTabItem GetTabItem(DataItemController controller);
        FrameworkElement GetPanel(DataItemController controller);
        Grid GetTabItemContent(DataItemController controller);
    }
}
