using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Windows;
using System.Windows.Media;
using InterfaceToDataBase;

namespace InterfaceToClient
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

        public static String Search(this DataItemController dataItemController, List<string> searchListParameter)
        {
            if (searchListParameter.Count != 0)
            {
                var searchList = new List<string>(searchListParameter);
                var resultCollection = dataItemController.GetSearchPropertyValueDic().Values.Where(value => value.ToLower().SearchContains(searchList)).ToArray();
                if (searchList.ToList().Count == 0)
                    return String.Join(" ", resultCollection);
            }
            return null;
        }

        public static void DisposeChildrenObservers(this FrameworkElement depObjParent)
        {    
            foreach (var depObjChild in LogicalTreeHelper.GetChildren(depObjParent))
            {
                if (depObjChild is FrameworkElement)
                    if (depObjChild is Observer)
                        ((Observer)depObjChild).Dispose();
                    else
                        DisposeChildrenObservers((FrameworkElement)depObjChild);
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
            var parent = VisualTreeHelper.GetParent(depObj);
            if (parent != null)
            {
                if (parent is T)
                    return (FrameworkElement)parent;
                else
                    return FindParentByType<T>((FrameworkElement)parent);
            }
            else return null;
        }

    }
}
