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
using System.Collections;
using System.Collections.Specialized;
using MiaMain;

namespace MiaAppInterface
{
    /// <summary>
    /// Логика взаимодействия для EventsListBox.xaml
    /// </summary>
    public partial class EventsListBox : Grid, Observer, IClose
    {
        DataItemsFactory Factory;
        Device Device;
        public EventsListBox()
        {
            InitializeComponent();
            ListBox.Items.Clear();
            Factory = FactoriesVault.FactoriesDic["DeviceEvents"] as DeviceEventsFactory;
            FactoriesVault.ChangesGetter.AddObserver(this, new string[] { "DeviceEvents" });
            FilterComboBox.SelectedIndex = 0;

        }

        IEnumerable ItemsSource { get { return ListBox.ItemsSource; } set { ListBox.ItemsSource = value; } }
        ItemCollection Items { get { return ListBox.Items; }  }

        private void ListBox_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext != null)
            {
                Device = (Device)((DataItemsChange)DataContext).NewDataItem;
                GetItems();
                
            }
        }

        private void ListBoxItem_DoubleClick(object sender, RoutedEventArgs e)
        {
            if (ListBox.SelectedItem != null)
            {
                var dataItem = (DataItem)ListBox.SelectedItem;
                
                e.Handled = true;
            }   
        }


        private void GetItems()
        {
            ItemsSource = Factory.GetDataItemsDic().Select(keyValuePair => (DeviceEvent)keyValuePair.Value).
                Where(deviceEvent => deviceEvent.DeviceId == Device.Id).
                Select(item => new TabItemGrid() { ContentGrid = new DeviceEventGrid(), DataContext = new DataItemsChange() { Action = NotifyCollectionChangedAction.Add, NewDataItem = item } });
        }

        public void Update(DataItemsChange Change)
        {
            foreach (ManagerGrid item in Items)
            {
                if (((Change.NewDataItem != null) && (Change.NewDataItem.Id == ((DataItemsChange)item.DataContext).NewDataItem.Id)) ||
                ((Change.OldDataItem != null) && (Change.OldDataItem.Id == ((DataItemsChange)item.DataContext).NewDataItem.Id)))
                {
                    if ((Change.Action == NotifyCollectionChangedAction.Remove))
                    {
                        Change.OldDataItem.Id = 0;
                        Change.NewDataItem = Change.OldDataItem;
                    }
                    item.DataContext = Change;
                    return;
                }
                item.RefreshDataContext(item.DataContext);
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedFilter = ((ComboBoxItem)FilterComboBox.SelectedItem).Content.ToString();
            if (selectedFilter != "Все события")
                Items.Filter = item => ((DeviceEvent)item).Type == selectedFilter;
            else
                Items.Filter = null;
        }

        public void Close(ManagerGrid manager)
        {
            Items.Remove(manager);
        }
    }
}
