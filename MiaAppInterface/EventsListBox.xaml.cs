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
    public partial class EventsListBox : Grid, Observer
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
                DeviceEventsTabControl.DataContext = new EventsController(Device.Id);
            }
        }

        private void ListBoxItem_DoubleClick(object sender, RoutedEventArgs e)
        {
            if (ListBox.SelectedItem != null)
            {
                var dataItem = (DataItem)ListBox.SelectedItem;
                OpenNewTab(dataItem);
                e.Handled = true;
            }   
        }

        private void OpenNewTab(DataItem dataItem)
        {
            var tabItem = DeviceEventsTabControl.GetTabItemByDataContext(dataItem);
            if (tabItem == null)
            {
                tabItem = ((DataItemsController)DeviceEventsTabControl.DataContext).GetTabItem(dataItem);
                DeviceEventsTabControl.Items.Add(tabItem);
            }
            tabItem.IsSelected = true;
        }

        private void GetItems()
        {
            ItemsSource = Factory.GetDataItemsDic().Select(keyValuePair => (DeviceEvent)keyValuePair.Value).
                Where(deviceEvent => deviceEvent.DeviceId == Device.Id).
                Select(item => new DataItemsChange() { Action = NotifyCollectionChangedAction.Add, NewDataItem = item});
        }

        public void Update(DataItemsChange Change)
        {
            if (((Change.NewDataItem != null) && (((DeviceEvent)Change.NewDataItem).DeviceId == Device.Id)) ||
                ((Change.OldDataItem != null) && (((DeviceEvent)Change.NewDataItem).DeviceId == Device.Id)))
            {
                GetItems();
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
    }
}
