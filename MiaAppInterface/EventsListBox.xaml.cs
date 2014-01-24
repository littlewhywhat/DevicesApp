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
using System.Collections.ObjectModel;

namespace MiaAppInterface
{
    /// <summary>
    /// Логика взаимодействия для EventsListBox.xaml
    /// </summary>
    public partial class EventsListBox : Grid, Observer, IClose, IDisposable
    {
        DataItemsFactory Factory;
        Device Device;
        ObservableCollection<ListBoxItemGrid> ListBoxItemsCollection = new ObservableCollection<ListBoxItemGrid>();
        EventsController controller;
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
                var device = (Device)((DataItemsChange)DataContext).NewDataItem;
                if (Device == null)
                {
                    
                    if (device.Id != 0)
                    {
                        Device = device;
                        IsEnabled = true;
                        GetItems();
                        controller = new EventsController(Device.Id);


                    }
                }
                else if (device.Id == 0)
                {
                    IsEnabled = false;
                    ListBoxItemsCollection.Clear();
                    Device = null;
                }
            }
        }

        private void ListBoxItem_DoubleClick(object sender, RoutedEventArgs e)
        {
            //if (ListBox.SelectedItem != null)
            //{
            //    var dataItem = (DataItem)ListBox.SelectedItem;
                
            //    e.Handled = true;
            //}   
        }


        private void GetItems()
        {
            Factory.GetDataItemsDic().Select(keyValuePair => (DeviceEvent)keyValuePair.Value).
                Where(deviceEvent => deviceEvent.DeviceId == Device.Id).
                ForEach(item => ListBoxItemsCollection.Add(GetNewItem(item)));
            ItemsSource = ListBoxItemsCollection;
        }

        private ListBoxItemGrid GetNewItem(DataItem item)
        {
            return new ListBoxItemGrid()
                {
                    Closer = this,
                    ContentGrid = new DeviceEventGrid(),
                    DataContext = new DataItemsChange() { Action = NotifyCollectionChangedAction.Add, NewDataItem = item }
                };
        }

        public void Update(DataItemsChange Change)
        {
            try
            {
                if (Device != null)
                {
                    ControlManager item = null;
                    switch (Change.Action)
                    {
                        case NotifyCollectionChangedAction.Replace:
                            item = GetRelatedListBoxItem(Change.NewDataItem);
                            if (item != null)
                            {
                                item.DataContext = Change;
                                Items.Filter = Items.Filter;
                            }
                            break;
                        case NotifyCollectionChangedAction.Remove:
                            item = GetRelatedListBoxItem(Change.OldDataItem);
                            if (item != null)
                            {
                                Change.OldDataItem.Id = 0;
                                Change.NewDataItem = Change.OldDataItem;
                                item.DataContext = Change;
                                Items.Filter = Items.Filter;
                            }
                            break;
                        case NotifyCollectionChangedAction.Add:
                            item = GetRelatedListBoxItem(Change.NewDataItem);
                            if (item != null)
                            {
                                item.DataContext = Change;
                                Items.Filter = Items.Filter;
                            }
                            else
                                if (IsRelatedByDeviceId((DeviceEvent)Change.NewDataItem))
                                    ListBoxItemsCollection.Add(GetNewItem(Change.NewDataItem));
                            break;
                    }
                }
            }
            catch (Exception)
            { }

        }

        private ControlManager GetRelatedListBoxItem(DataItem dataItem)
        {
            ControlManager listBoxItem = null;
            if (IsRelatedByDeviceId((DeviceEvent)dataItem))
                foreach (ControlManager item in ListBoxItemsCollection)
                {
                    if (dataItem.Id != ((DataItemsChange)item.DataContext).NewDataItem.Id)
                    {
                        item.RefreshDataContext(item.DataContext);
                    }
                    else
                        listBoxItem = item;
                }
            return listBoxItem;
        }
        private bool IsRelatedByDeviceId(DeviceEvent deviceEvent)
        {
            return deviceEvent.DeviceId == Device.Id;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedFilter = ((ComboBoxItem)FilterComboBox.SelectedItem).Content.ToString();
            if (selectedFilter != "Все события")
                Items.Filter = item => ((DeviceEvent)((ListBoxItemGrid)item).CurrentDataItem).Type == selectedFilter;
            else
                Items.Filter = null;
        }

        public void Close(ControlManager manager)
        {
            ListBoxItemsCollection.Remove((ListBoxItemGrid)manager);
        }

        private void Insert_Click(object sender, RoutedEventArgs e)
        {
            var deviceEvent = (DeviceEvent)controller.GetNewDataItem();
            var selectedFilter = ((ComboBoxItem)FilterComboBox.SelectedItem).Content.ToString();
            if (selectedFilter != "Все события")
                deviceEvent.Type = selectedFilter;
            ListBoxItemsCollection.Add(GetNewItem(deviceEvent));
        }





        public void Dispose()
        {
            FactoriesVault.ChangesGetter.RemoveObserver(this);
            this.DisposeChildren();
        }
    }
}
