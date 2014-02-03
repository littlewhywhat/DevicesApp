using System;
using System.Collections.Generic;
using System.Linq;
using DataItemControllersLibrary;
using System.Windows;
using System.Windows.Controls;

namespace InterfaceToClient
{
    public static class ControllersExtensions
    {
        public static IExtDataItemControllersFactory GetExtFactory(this DataItemController controller)
        {
            return (IExtDataItemControllersFactory)controller.Factory;
        }
        public static DataItemsTabItem GetInsertTabItem(this DataItemControllersFactory factory)
        {
            return factory.GetControllerDefault().GetTabItem();
        }
        public static DataItemsTabItem GetTabItem(this DataItemController controller)
        {
            return controller.GetExtFactory().GetTabItem(controller);
        }

        public static ListBoxItemGrid GetListBoxItemGrid(this DataItemController controller)
        {
            return new ListBoxItemGrid() { ContentGrid = controller.GetExtFactory().GetDataItemsInfoGrid(controller), DataContext = controller };
        }

        public static DataItemsTabItem GetTabItemDefault(this DataItemController controller)
        {
            var tabItem = new DataItemsTabItem() { DataContext = controller, Content = controller.GetExtFactory().GetTabItemContent(controller) };
            Vault.ChangesGetter.AddObserver(tabItem, new string[] { controller.Factory.TableName });
            return tabItem;
        }
        public static FrameworkElement GetPanel(this DataItemController controller, string Tag)
        {
            var panel =controller.GetExtFactory().GetPanel(controller);
            panel.Tag = Tag;
            panel.DataContext = controller;
            return panel;
        }

        public static Grid GetTabItemContentDefault(this DataItemController controller)
        {
            var dataGrid = new TabItemGrid();
            dataGrid.ContentGrid = controller.GetExtFactory().GetDataItemsInfoGrid(controller);
            return dataGrid;
        }
    }
}
