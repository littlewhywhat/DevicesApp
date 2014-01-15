using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Threading;
using MiaMain;
using System.Collections.ObjectModel;
namespace MiaAppInterface
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class DataItemsExplorer : Window
    {
        DataItemsController Controller;
        
        public DataItemsExplorer(DataItemsController controller)
        {
            Controller = controller;
            Initialize();
        }
        public void Initialize()
        {
            InitializeComponent();
            DataItemsTree.BuildTree(Controller);
        }

        private void DataItemsTreeView_DoubleClick (object sender, MouseButtonEventArgs e)
        {
            if (DataItemsTree.SelectedItem != null)
            {
                var dataItem = (DataItem)(DataItemsTree.SelectedItem as TreeViewItem).DataContext;
                OpenNewTab(dataItem);
                e.Handled = true;
            }   
        }
        private void OpenNewTab (DataItem dataItem)
        {
            var tabItem = DataItemsTabControl.GetTabItemByDataContext(dataItem.Id);
            if (tabItem == null)
            {
                tabItem = Controller.GetTabItem(dataItem);
                DataItemsTabControl.Items.Add(tabItem);
            }
            tabItem.IsSelected = true;
        }

        private void Insert_Click(object sender, MouseButtonEventArgs e)
        {
            var tabItem = Controller.GetTabItem();
            DataItemsTabControl.Items.Add(tabItem);
            tabItem.IsSelected = true;            
        }


        private void DataItemsTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var tabControl = sender as TabControl;
            if (!(tabControl.SelectedItem is DataItemsTabItem))
                if (e.RemovedItems.Count != 0)
                    ((DataItemsTabItem)e.RemovedItems[0]).IsSelected = true;
        }

        private void SearchGrid_KeyUp(object sender, KeyEventArgs e)
        {
            if (sender is SearchGrid)
            {
                var searchGrid = sender as SearchGrid;
                var dataItem = ((ListBoxItem)searchGrid.searchPopUp.searchListBox.SelectedItem).Tag as DataItem;
                OpenNewTab(dataItem);
            }
        }
    }
}
