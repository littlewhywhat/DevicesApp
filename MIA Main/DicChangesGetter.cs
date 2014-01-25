using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;
using System.Collections.Specialized;
using System.Windows;

namespace MiaMain
{
    public class DicChangesGetter
    {
        Dictionary<string, List<Observer>> ListsDic = new Dictionary<string, List<Observer>>();
        public DicChangesGetter()
        {
            FactoriesVault.FactoriesDic[TableNames.Companies].GetDataItemsDic().CollectionChanged += CompaniesDicCollectionChanged;
            ListsDic.Add(TableNames.Companies, new List<Observer>());
            FactoriesVault.FactoriesDic[TableNames.Devices].GetDataItemsDic().CollectionChanged += DevicesDicCollectionChanged;
            ListsDic.Add(TableNames.Devices.ToString(), new List<Observer>());
            FactoriesVault.FactoriesDic[TableNames.DeviceEvents].GetDataItemsDic().CollectionChanged += DeviceEventsDicCollectionChanged;
            ListsDic.Add(TableNames.DeviceEvents, new List<Observer>());
            FactoriesVault.FactoriesDic[TableNames.DeviceTypes].GetDataItemsDic().CollectionChanged += DeviceTypesDicCollectionChanged;
            ListsDic.Add(TableNames.DeviceTypes, new List<Observer>());
        }

        public void AddObserver(Observer observer, IEnumerable<string> listOfDics)
        {
            
            listOfDics.ForEach(dicName => ListsDic[dicName].Add(observer));
        }


        public void RemoveObserver(Observer observer)
        {
            ListsDic.Select(keyValuePair => keyValuePair.Value).ToList().ForEach(observerList =>
            {
                if (observerList.Contains(observer))
                    observerList.Remove(observer);
            });
            
        }

        private void DevicesDicCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            ProcessListOfObservers(TableNames.Devices.ToString(), e);
        }

        private void CompaniesDicCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            ProcessListOfObservers(TableNames.Companies.ToString(), e);
        }

        private void DeviceEventsDicCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            ProcessListOfObservers(TableNames.DeviceEvents.ToString(), e);
        }

        private void DeviceTypesDicCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            ProcessListOfObservers(TableNames.DeviceTypes.ToString(), e);
        }

        private void ProcessListOfObservers(string listName, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action != NotifyCollectionChangedAction.Reset)
                ListsDic[listName].ForEach(observer =>
                {
                    DataItemsChange Change = new DataItemsChange() { Action = e.Action };
                    if ((e.OldItems != null) && (e.OldItems.Count != 0))
                        Change.OldDataItem = ((KeyValuePair<int, DataItem>)e.OldItems[0]).Value;
                    if ((e.NewItems != null) && (e.NewItems.Count != 0))
                    {
                        Change.NewDataItem = ((KeyValuePair<int, DataItem>)e.NewItems[0]).Value;
                        Change.NewDataItem.Fill(Change.NewDataItem.Factory.OtherTableFields);
                    }
                    PerformActionOnObserver(observer, () => observer.Update(Change));
                });
        }

        private void PerformActionOnObserver(Observer observer, Action action)
        {
            var dispObj = observer as DispatcherObject;
            if (dispObj != null)
            {
                Dispatcher dispatcher = dispObj.Dispatcher;
                if (dispatcher != null && !dispatcher.CheckAccess())
                {
                    dispatcher.Invoke(action,
                        DispatcherPriority.ApplicationIdle);
                }
            }
            else
                action.Invoke();
        }
    }
}
