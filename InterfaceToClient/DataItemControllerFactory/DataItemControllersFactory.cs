using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using InterfaceToDataBase;
using System.Windows.Controls;

namespace InterfaceToClient
{
    public abstract class DataItemControllersFactory
    {
        EventHandler<DataItemControllerChangedEventArgs> CollectionChanged;
        public DataItemsDictionary DataItemsDic;
        public DataItemControllersFactory(DataItemsFactory factory)
        {
            DataItemsDic = GetDataItemsDictionary(factory);
            DataItemsDic.SetCollectionChangedHandler(DataItemsDic_CollectionChanged);
        }
        protected abstract DataItemsDictionary GetDataItemsDictionary(DataItemsFactory factory);


        public string TableName { get { return DataItemsDic.TableName; } }

        private void DataItemsDic_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action != NotifyCollectionChangedAction.Reset)
                if (CollectionChanged != null)
                {
                    DataItemControllerChangedEventArgs Change = new DataItemControllerChangedEventArgs() { Action = e.Action };
                    if ((e.OldItems != null) && (e.OldItems.Count != 0))
                        Change.OldController = GetController(((KeyValuePair<int, DataItem>)e.OldItems[0]).Value);
                    if ((e.NewItems != null) && (e.NewItems.Count != 0))
                        Change.NewController = GetController(((KeyValuePair<int, DataItem>)e.NewItems[0]).Value);
                    CollectionChanged.GetInvocationList().ToList().ForEach(handler =>
                        ((EventHandler<DataItemControllerChangedEventArgs>)handler).Invoke(this, Change));
                }
        }
        public void SetCollectionChangedHandler(EventHandler<DataItemControllerChangedEventArgs> handler)
        {
            CollectionChanged += handler;
        }
        
        public DataItemController GetDataItemController(int Id)
        {
            var dataItem = DataItemsDic.GetDataItemById(Id);
            if (dataItem != null)
                return GetController(dataItem);
            else
                return null;
        }

        private IEnumerable<DataItemController> PackInControllers( IEnumerable<DataItem> dataItems)
        {
            return dataItems.Select(dataItem => GetController(dataItem));
        }

        public abstract DataItemController GetController(DataItem dataItem);
        
        public DataItemController GetEmptyController()
        {
            return GetController(DataItemsDic.GetEmptyDataItem());
        }

        internal virtual IEnumerable<DataItemController> GetPossibleParents(DataItemController dataItemController)
        {
            return null;
        }

        internal virtual List<DataItemController> GetChildren(DataItemController dataItemController)
        {
            return PackInControllers(DataItemsDic.GetChildrenByParentId(dataItemController.Id)).ToList();
        }

        public IEnumerable<SearchResult> Search(List<string> searchList)
        {
            return DataItemsDic.GetValues().Select(dataItem => GetController(dataItem))
                .Select(dataItemController => new SearchResult(dataItemController.Search(searchList), dataItemController));
        }

        internal virtual DataItemsTabItem GetTabItem(DataItemController dataItemController)
        {
            return GetTabItemByDataItem(dataItemController);
        }

        protected abstract Grid GetTabItemContent();

        private DataItemsTabItem GetTabItemByDataItem(DataItemController dataItemController)
        {
            var tabItem = new DataItemsTabItem() { DataContext = new DataItemControllerChangedEventArgs() { NewController = dataItemController }, Content = GetTabItemContent() };
            FactoriesVault.ChangesGetter.AddObserver(tabItem, new string[] { TableName });
            return tabItem;
        }



        public DataItemsTabItem GetInsertTabItem()
        {

            //var managerGrid = tabItem.Content as ManagerGrid;
            //managerGrid.DataContext = new DataItemsChange() { NewDataItem = dataItem };

            //managerGrid.EnableInsertMode();
            return GetTabItemByDataItem(GetEmptyController());
        }
               
    }
}
