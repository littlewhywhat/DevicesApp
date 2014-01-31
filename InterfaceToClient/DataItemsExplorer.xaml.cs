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
using InterfaceToDataBase;
using System.Collections.ObjectModel;
namespace InterfaceToClient
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
            DataItemsTree.BuildTree((DataItemControllersDictionary)DataContext);
        }

        private void DataItemsTreeView_DoubleClick (object sender, MouseButtonEventArgs e)
        {
            if (DataItemsTree.SelectedItem != null)
            {
                var dataItemController = (DataItemController)((TreeViewItem)DataItemsTree.SelectedItem).DataContext;
                OpenNewTab(dataItemController);
                e.Handled = true;
            }   
        }

        private void OpenNewTab (DataItemController dataItemController)
        {
            var tabItem = DataItemsTabControl.GetTabItemByDataContext(dataItemController);
            if (tabItem == null)
            {
                tabItem = dataItemController.GetTabItem();
                DataItemsTabControl.Items.Add(tabItem);
            }
            tabItem.IsSelected = true;
        }




        private void SearchGrid_KeyUp(object sender, KeyEventArgs e)
        {
            if (sender is SearchGrid && e.Key == Key.Enter)
            {
                var searchGrid = (SearchGrid)sender;
                var dataItemController = (DataItemController)((FrameworkElement)searchGrid.searchPopUp.searchListBox.SelectedItem).DataContext;
                OpenNewTab(dataItemController);
            }
        }

        private void ControllerComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataContext = FactoriesVault.Dic[((ComboBoxItem)ControllerComboBox.SelectedItem).Name];
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }


    }
}
