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
        private DataItemsGrid DataItemsGrid
        {
            get { return contentGrid.Children[0] as DataItemsGrid; }
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
            SwitchChangeMode(false);
            ((DataItem)DataItemsGrid.DataContext).ChangeInDb();
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            var dataItem = DataContext as DataItem;
            var dataItemNew = dataItem.Clone();
            SwitchChangeMode(true);
            DataItemsGrid.Refresh(dataItemNew);
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            SwitchChangeMode(false);
            DataItemsGrid.Refresh(DataContext);
        }        

        public void SwitchChangeMode(bool position)
        {
            DataItemsGrid.ChangeMode = position;
            Update.IsEnabled = position;
            Delete.IsEnabled = position;
            Change.IsEnabled = !position;
            Cancel.IsEnabled = position;
            contentGrid.IsEnabled = position;
        }

        public void EnableInsertMode()
        {
            SwitchChangeMode(true);
            Cancel.IsEnabled = false;
            Delete.IsEnabled = false;
        }

        private void Grid_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (!DataItemsGrid.ChangeMode)
                DataItemsGrid.Refresh(DataContext);
            else
                DataItemsGrid.RefreshComboBoxes();
        }


    }
}
