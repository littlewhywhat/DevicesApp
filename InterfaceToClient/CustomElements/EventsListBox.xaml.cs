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
using DataItemsLibrary;
using System.Collections.ObjectModel;
using DataItemControllersLibrary;

namespace InterfaceToClient
{
    /// <summary>
    /// Логика взаимодействия для EventsListBox.xaml
    /// </summary>
    public partial class EventsListBox : Grid, IObserver, IClose
    {
        ObservableCollection<ListBoxItemGrid> EventsCollection = new ObservableCollection<ListBoxItemGrid>();
        const string _AllEventsFilter = "Все события";
        private string CurrentFilter { get { return ((ComboBoxItem)FilterComboBox.SelectedItem).Content.ToString(); } }
        private bool IsCurrentFilterAllEvents { get { return CurrentFilter == _AllEventsFilter; } }

        public EventsListBox()
        {
            InitializeComponent();
            ItemsSource = EventsCollection;
            Vault.ChangesGetter.AddObserver(this, new string[] { TableNames.DeviceEvents });
            FilterComboBox.SelectedIndex = 0;
            
        }
        public DeviceController DeviceControllerDataContext { get { return (DeviceController)DataContext; } }
        IEnumerable ItemsSource { get { return ListBox.ItemsSource; } set { ListBox.ItemsSource = value; } }
        ItemCollection Items { get { return ListBox.Items; }  }

        private void GetItems()
        {
            DeviceControllerDataContext.GetEvents().ToList().
                ForEach(item => EventsCollection.Add(item.GetListBoxItemGrid()));
        }
        private void RefreshFilter()
        {
            Items.Filter = Items.Filter;
        }

        public void Update(DataItemControllerChangedEventArgs Change)
        {
            try
            {
                if (DeviceControllerDataContext.EventIsRelated((DeviceEventController)Change.NewController) 
                    || DeviceControllerDataContext.EventIsRelated((DeviceEventController)Change.OldController))
                { 
                    ControlManager item = GetRelatedListBoxItem(DeviceControllerDataContext);
                    switch (Change.Action)
                    {
                        case NotifyCollectionChangedAction.Replace:
                            item = GetRelatedListBoxItem(Change.NewController);
                            item.CurrentDataItemController.RefreshDataItemByController(Change.NewController);
                            break;
                        case NotifyCollectionChangedAction.Remove:
                            item = GetRelatedListBoxItem(Change.OldController);
                            if (item != null)
                                item.CurrentDataItemController.GetInInsertMode();
                            break;
                        case NotifyCollectionChangedAction.Add:
                            item = GetRelatedListBoxItem(Change.NewController);
                            if (item == null)
                                EventsCollection.Add(Change.NewController.GetListBoxItemGrid());
                            break;
                    }
                    RefreshFilter();
                }
            }
            catch (Exception)
            { }
        }

        private ControlManager GetRelatedListBoxItem(DataItemController dataItemController)
        {
            foreach (ControlManager item in EventsCollection)
                if (dataItemController != item.CurrentDataItemController)
                    item.CurrentDataItemController.Refresh();
                else
                    return item;
            return null;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!IsCurrentFilterAllEvents)
                Items.Filter = item => ((DeviceEventController)((ListBoxItemGrid)item).CurrentDataItemController).Type == CurrentFilter;
            else
                Items.Filter = null;
        }

        public void Close(ControlManager manager)
        {
            EventsCollection.Remove((ListBoxItemGrid)manager);
        }

        private void Insert_Click(object sender, RoutedEventArgs e)
        {
            var deviceEvent = DeviceControllerDataContext.GetNewEventController();
            AssignCurrentFilter(deviceEvent);
            EventsCollection.Add(deviceEvent.GetListBoxItemGrid());
        }

        private void AssignCurrentFilter(DeviceEventController deviceEvent)
        {
            var selectedFilter = ((ComboBoxItem)FilterComboBox.SelectedItem).Content.ToString();
            if (selectedFilter != "Все события")
                deviceEvent.Type = selectedFilter;
        }

        public void Dispose()
        {
            Vault.ChangesGetter.RemoveObserver(this);
            this.DisposeChildrenObservers();
        }

        private void Events_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (IsEnabled)
                GetItems();
            else
                EventsCollection.Clear();
        }

        
    }
}
