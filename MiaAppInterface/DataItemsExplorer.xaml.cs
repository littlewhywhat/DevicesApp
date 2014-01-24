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
        
        public DataItemsExplorer()
        {
            InitializeComponent();
            ControllerComboBox.SelectedIndex = 0;
        }
        
        private void Window_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            DataItemsTree.BuildTree((DataItemsController)DataContext);
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
            var tabItem = DataItemsTabControl.GetTabItemByDataContext(dataItem);
            if (tabItem == null)
            {
                tabItem = ((DataItemsController)DataContext).GetTabItem(dataItem);
                DataItemsTabControl.Items.Add(tabItem);
            }
            tabItem.IsSelected = true;
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

        private void ControllerComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataContext = Controller.GetController(((ComboBoxItem)ControllerComboBox.SelectedItem).Name);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }


    }
}
