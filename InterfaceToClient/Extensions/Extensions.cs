using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Windows;
using System.Windows.Media;
using DataItemsLibrary;
using DataItemControllersLibrary;

namespace InterfaceToClient
{
    static class Extensions
    {      

        public static void DisposeChildrenObservers(this FrameworkElement depObjParent)
        {    
            foreach (var depObjChild in LogicalTreeHelper.GetChildren(depObjParent))
            {
                if (depObjChild is FrameworkElement)
                    if (depObjChild is IObserver)
                        ((IObserver)depObjChild).Dispose();
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
