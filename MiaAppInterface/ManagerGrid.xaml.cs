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
using System.Collections.Specialized;
using System.ComponentModel;
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
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public Grid ContentGrid { get { return contentGrid; } set { contentGrid.Children.Add(value); } }

        private DataItemsInfoGrid DataItemsGrid
        {
            get { return contentGrid.Children[0] as DataItemsInfoGrid; }
        }
        private DataItem CurrentDataItem
        {
            get { return ((DataItemsChange)DataContext).NewDataItem; }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            CurrentDataItem.Delete();
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
            var dataItemNew = CurrentDataItem.Clone();
            SwitchChangeMode(true);
            DataItemsGrid.Refresh(dataItemNew);
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            SwitchChangeMode(false);
            DataItemsGrid.Refresh(CurrentDataItem);
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
            if (DataContext != null)
            {
                var Change = (DataItemsChange)DataContext;
                if (Change.Action == NotifyCollectionChangedAction.Remove)
                {
                    DataItemsGrid.Refresh(Change.NewDataItem);
                    EnableInsertMode();
                }
                if (!DataItemsGrid.ChangeMode)
                    DataItemsGrid.Refresh(Change.NewDataItem);
                else
                    DataItemsGrid.RefreshComboBoxes();
            }
        }


    }
}
