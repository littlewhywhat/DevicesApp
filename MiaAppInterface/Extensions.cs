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
        public static bool SearchContains(this string value, List<string> searchList)
        {
            bool result = false;
            for (int i = 0; i < searchList.Count; i++)
            {
                var listItem = searchList[i];
                if (value.Contains(listItem))
                {
                    result = true;
                    searchList.Remove(listItem);
                    i--;
                }
            }
            return result;
        }

        public static String Search(this DataItem dataItem, List<string> searchList)
        {
            var resultCollection = dataItem.GetSearchPropertyValueDic().Values.Where( value => value.ToLower().SearchContains(searchList)).ToArray();
            if (searchList.ToList().Count == 0)
                return String.Join(" ", resultCollection);
            return null;
        }

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

        public static FrameworkElement FindFirstChild<T>(this FrameworkElement depObjParent)
        {
            foreach (var depObjChild in LogicalTreeHelper.GetChildren(depObjParent))
                if (depObjChild is FrameworkElement)
                    if (depObjChild is T)
                        return (FrameworkElement)depObjChild;
                    else
                    {
                        var firstChild = FindFirstChild<T>((FrameworkElement)depObjChild);
                        if (firstChild != null)
                            return firstChild;
                    }
            return null;
        }


        public static FrameworkElement FindParentByType<T>(this FrameworkElement depObj)
        {
            if (depObj.Parent != null)
            {
                
                if (depObj.Parent is T)
                    return (FrameworkElement)depObj.Parent;
                else
                    return FindParentByType<T>((FrameworkElement)depObj.Parent);
            }
            else return null;
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
