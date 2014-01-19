using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MiaMain;

namespace MiaAppInterface
{
    /// <summary>
    /// Логика взаимодействия для DataItemsTreeViewItem.xaml
    /// </summary>
    public partial class DataItemsTreeViewItem : TreeViewItem
    {
        public DataItemsTreeViewItem()
        {
            InitializeComponent();
            MouseDoubleClick += new MouseButtonEventHandler((sender, e) => { });
        }

        private void Node_Collapsed(object sender, RoutedEventArgs e)
        {
            var mainItem = sender as TreeViewItem;
            foreach (TreeViewItem item in mainItem.Items)
            {
                item.Items.Clear();
            }
            e.Handled = true;
        }

        private void Node_Expanded(object sender, RoutedEventArgs e)
        {
            var mainItem = sender as TreeViewItem;
            foreach (TreeViewItem item in mainItem.Items)
            {
                var dataItem = (DataItem)item.DataContext;
                var parent = RootTree() as DataItemsTreeView;
                parent.GetNodesFromDicWithParentId(dataItem.Id).ForEach(child => item.Items.Add(child));
            }
            e.Handled = true;
        }

        private DataItemsTreeView RootTree()
        {
            var parent = this.Parent;
            while (!(parent is DataItemsTreeView))
                parent = ((ItemsControl)parent).Parent;
            return (DataItemsTreeView)parent;
        }
    }
}
