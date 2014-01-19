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
    /// Логика взаимодействия для DataGrid.xaml
    /// </summary>
    public partial class ManagerGrid : Grid
    {
        public ManagerGrid()
        {
            InitializeComponent();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var dataItem = DataContext as DataItem;
            dataItem.Delete();
            var tabItem = this.Parent as DataItemsTabItem;
            tabItem.CloseTabItem();
            e.Handled = true;
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            UnenableChangeMode(true);
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            var dataItem = DataContext as DataItem;
            var dataItemNew = dataItem.Clone();
            EnableChangeMode(dataItemNew);
        }
        public void UnenableChangeMode(bool updateDataItem)
        {
            var dataItemsGrid = GetDataItemsGrid();
            dataItemsGrid.ChangeMode = false;
            if (updateDataItem)
                dataItemsGrid.UpdateDataItem();
            else
                dataItemsGrid.RefreshDataContext(DataContext);
            Update.IsEnabled = false;
            Delete.IsEnabled = false;
            Change.IsEnabled = true;
            Cancel.IsEnabled = false;
            contentGrid.IsEnabled = false;
        }
        public void EnableInsertMode()
        {
            EnableChangeMode((DataItem)((TabItem)Parent).DataContext);
            Cancel.IsEnabled = false;
            Delete.IsEnabled = false;
        }
        public void EnableChangeMode(DataItem dataItemNew)
        {
            var dataItemGrid = GetDataItemsGrid();
            dataItemGrid.ChangeMode = true;
            dataItemGrid.RefreshDataContext(dataItemNew);
            Update.IsEnabled = true;
            Delete.IsEnabled = true;
            Change.IsEnabled = false;
            Cancel.IsEnabled = true;
            contentGrid.IsEnabled = true;
        }
        private DataItemsGrid GetDataItemsGrid()
        {
            return contentGrid.Children[0] as DataItemsGrid;
        }

        private void Grid_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var dataItemsGrid = GetDataItemsGrid();
            if (!dataItemsGrid.ChangeMode)
                dataItemsGrid.RefreshDataContext(DataContext);
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            UnenableChangeMode(false);
        }
    }
}
