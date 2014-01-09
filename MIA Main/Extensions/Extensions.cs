using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Threading;

namespace MiaMain
{
    public static class Extensions
    {
        //public static void ForEach(this ItemCollection collection, Action<T> action)
        //{
        //    foreach (T item in enumerable)
        //    {
        //        action(item);
        //    }
        //}
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach(T item in enumerable)
            {
                action(item);
            }
        }
        public static TabItem GetTabItemByDataContext(this TabControl tabControl, int target)
        {
            foreach (TabItem item in tabControl.Items)
            {
                DataItem dataContext = null;
                if (Thread.CurrentThread.Equals(item.Dispatcher.Thread))
                    dataContext = (DataItem)item.DataContext;
                else
                {
                    item.Dispatcher.BeginInvoke(new ThreadStart(() => dataContext = (DataItem)item.DataContext));
                    while (dataContext == null) ;
                }
                if (dataContext.Id == target)
                    return item;
            }
            return null;
        }

        public static Dictionary<string, string> GetPropertyValueDic(this DataItem dataItem)
        {
            var dictionary = new Dictionary<string, string>();
            dataItem.GetType().GetProperties().ForEach(dataItemProperty =>
                {
                    if (dataItemProperty.Name != "Id")
                        dictionary.Add(dataItemProperty.Name, dataItemProperty.GetValue(dataItem).ToString());
                });
            return dictionary;
        }
    }

}
