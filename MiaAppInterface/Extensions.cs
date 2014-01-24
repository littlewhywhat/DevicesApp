using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Windows;
using System.Windows.Media;
using MiaMain;

namespace MiaAppInterface
{
    static class Extensions
    {
        public static void DisposeChildren(this FrameworkElement depObjParent)
        {
            
            foreach (var depObjChild in LogicalTreeHelper.GetChildren(depObjParent))
            {
                if (depObjChild is FrameworkElement)
                    if (depObjChild is IDisposable)
                        ((IDisposable)depObjChild).Dispose();
                    else
                        DisposeChildren((FrameworkElement)depObjChild);
            }
        }

        public static void RefreshDataContext(this FrameworkElement element, object DataContext)
        {
            element.DataContext = null;
            element.DataContext = DataContext;
        }
        public static TabItem GetTabItemByDataContext(this TabControl tabControl, DataItem dataItem)
        {
            foreach (TabItem item in tabControl.Items)
            {
                if (item is DataItemsTabItem)
                {
                    DataItemsChange dataContext = null;
                    if (item.Dispatcher.CheckAccess())
                        dataContext = (DataItemsChange)item.DataContext;
                    else
                    {
                        item.Dispatcher.BeginInvoke(new ThreadStart(() => dataContext = (DataItemsChange)item.DataContext));
                        while (dataContext == null) ;
                    }
                    if (dataContext.NewDataItem.Equals(dataItem))
                        return item;
                }
            }
            return null;
        }
        public static void ForEach(this ItemCollection enumerable, Action<ItemsControl> action)
        {
            foreach (ItemsControl item in enumerable)
            {
                action(item);
            }
        }
    }
}
