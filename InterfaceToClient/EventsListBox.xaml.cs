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
using InterfaceToDataBase;
using System.Collections.ObjectModel;

namespace InterfaceToClient
{
    /// <summary>
    /// Логика взаимодействия для EventsListBox.xaml
    /// </summary>
    public partial class EventsListBox : Grid, Observer, IClose
    {
        ObservableCollection<ListBoxItemGrid> EventsCollection = new ObservableCollection<ListBoxItemGrid>();
       
        public EventsListBox()
        {
            InitializeComponent();
            ListBox.Items.Clear();
            FactoriesVault.ChangesGetter.AddObserver(this, new string[] { TableNames.DeviceEvents });
            
            FilterComboBox.SelectedIndex = 0;

        }
        //private bool DeviceControllerIsSet { get { return DeviceControllerDataContext != null;  } }
        public DeviceController DeviceControllerDataContext { get { return (DeviceController)DataContext; } }
        IEnumerable ItemsSource { get { return ListBox.ItemsSource; } set { ListBox.ItemsSource = value; } }
        ItemCollection Items { get { return ListBox.Items; }  }

        private void GetItems()
        {
            DeviceControllerDataContext.GetEvents().ToList().
                ForEach(item => EventsCollection.Add(GetNewItem(item)));
            ItemsSource = EventsCollection;
        }

        private ListBoxItemGrid GetNewItem(DataItemController item)
        {
            return new ListBoxItemGrid()
                {
                    Closer = this,
                    ContentGrid = new DeviceEventGrid(),
                    DataContext = new DataItemControllerChangedEventArgs() { Action = NotifyCollectionChangedAction.Add, NewController = item }
                };
        }

        public void Update(DataItemControllerChangedEventArgs Change)
        {
            try
            {
                if (DataContext != null)
                {
                    ControlManager item = null;
                    switch (Change.Action)
                    {
                        case NotifyCollectionChangedAction.Replace:
                            item = GetRelatedListBoxItem(Change.NewController);
                            if (item != null)
                            {
                                item.DataContext = Change;
                                Items.Filter = Items.Filter;
                            }
                            break;
                        case NotifyCollectionChangedAction.Remove:
                            item = GetRelatedListBoxItem(Change.OldController);
                            if (item != null)
                            {
                                Change.OldController.GetInInsertMode();
                                Change.NewController = Change.OldController;
                                item.DataContext = Change;
                                Items.Filter = Items.Filter;
                            }
                            break;
                        case NotifyCollectionChangedAction.Add:
                            item = GetRelatedListBoxItem(Change.NewController);
                            if (item != null)
                            {
                                item.DataContext = Change;
                                Items.Filter = Items.Filter;
                            }
                            else
                                if (IsRelatedByDeviceId((DeviceEventController)Change.NewController))
                                    EventsCollection.Add(GetNewItem(Change.NewController));
                            break;
                    }
                }
            }
            catch (Exception)
            { }

        }

        private ControlManager GetRelatedListBoxItem(DataItemController dataItemController)
        {
            ControlManager listBoxItem = null;
            if (IsRelatedByDeviceId((DeviceEventController)dataItemController))
                foreach (ControlManager item in EventsCollection)
                {
                    if (dataItemController.Id != ((DataItemControllerChangedEventArgs)item.DataContext).NewController.Id)
                    {
                        item.RefreshDataContext(item.DataContext);
                    }
                    else
                        listBoxItem = item;
                }
            return listBoxItem;
        }
        private bool IsRelatedByDeviceId(DeviceEventController deviceEventController)
        {
            return deviceEventController.Device == DeviceControllerDataContext.Id;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedFilter = ((ComboBoxItem)FilterComboBox.SelectedItem).Content.ToString();
            if (selectedFilter != "Все события")
                Items.Filter = item => ((DeviceEventController)((ListBoxItemGrid)item).CurrentDataItemController).Type == selectedFilter;
            else
                Items.Filter = null;
        }

        public void Close(ControlManager manager)
        {
            EventsCollection.Remove((ListBoxItemGrid)manager);
        }

        private void Insert_Click(object sender, RoutedEventArgs e)
        {
            var deviceEvent = (DeviceEventController)FactoriesVault.Dic[TableNames.DeviceEvents].GetEmptyController();
            var selectedFilter = ((ComboBoxItem)FilterComboBox.SelectedItem).Content.ToString();
            if (selectedFilter != "Все события")
                deviceEvent.Type = selectedFilter;
            EventsCollection.Add(GetNewItem(deviceEvent));
        }

        public void Dispose()
        {
            FactoriesVault.ChangesGetter.RemoveObserver(this);
            this.DisposeChildren();
        }

        private void Events_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (IsEnabled)
                GetItems();
            else
                Items.Clear();
        }
    }
}
