using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace MiaMain
{
    public static class Extensions
    {
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach(T item in enumerable)
            {
                action(item);
            }
        }
        public static bool Contains(this TabControl tabControl, object target)
        {
            foreach (TabItem item in tabControl.Items)
            {
                if (item.DataContext == target)
                {
                    item.IsSelected = true;
                    return true;
                }                    
            }
            return false;
        }
    }

}
