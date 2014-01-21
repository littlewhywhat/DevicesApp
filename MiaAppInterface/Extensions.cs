using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Windows;
using MiaMain;

namespace MiaAppInterface
{
    static class Extensions
    {
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
                    DataItem dataContext = null;
                    if (item.Dispatcher.CheckAccess())
                        dataContext = (DataItem)item.DataContext;
                    else
                    {
                        item.Dispatcher.BeginInvoke(new ThreadStart(() => dataContext = (DataItem)item.DataContext));
                        while (dataContext == null) ;
                    }
                    if (dataContext.Equals(dataItem))
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
