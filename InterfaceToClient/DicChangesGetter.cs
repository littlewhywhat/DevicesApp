using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;
using System.Collections.Specialized;
using System.Windows;
using DataItemsLibrary;

namespace InterfaceToClient
{
    public class DicChangesGetter
    {
        Dictionary<string, List<IObserver>> ListsDic = new Dictionary<string, List<IObserver>>();
        public DicChangesGetter()
        {
            FactoriesVault.Dic[TableNames.Devices].DataItemControllersDic.CollectionChanged += DevicesDicCollectionChanged;
            ListsDic.Add(TableNames.Devices, new List<IObserver>());
            FactoriesVault.Dic[TableNames.DeviceTypes].DataItemControllersDic.CollectionChanged += DeviceTypesDicCollectionChanged;
            ListsDic.Add(TableNames.DeviceTypes, new List<IObserver>());
            FactoriesVault.Dic[TableNames.DeviceEvents].DataItemControllersDic.CollectionChanged += DeviceEventsDicCollectionChanged;
            ListsDic.Add(TableNames.DeviceEvents, new List<IObserver>());
            FactoriesVault.Dic[TableNames.Companies].DataItemControllersDic.CollectionChanged += CompaniesDicCollectionChanged;
            ListsDic.Add(TableNames.Companies, new List<IObserver>());
        }

        public void AddObserver(IObserver observer, IEnumerable<string> listOfDics)
        {
            listOfDics.ToList().ForEach(dicName => ListsDic[dicName].Add(observer));
        }


        public void RemoveObserver(IObserver observer)
        {
            ListsDic.Select(keyValuePair => keyValuePair.Value).ToList().ForEach(observerList =>
            {
                if (observerList.Contains(observer))
                    observerList.Remove(observer);
            });   
        }

        private void DevicesDicCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            ProcessListOfObservers(TableNames.Devices, InterpretChangedEventArgs(e, TableNames.Devices));
        }

        private void CompaniesDicCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            ProcessListOfObservers(TableNames.Companies, InterpretChangedEventArgs(e, TableNames.Companies));
        }

        private void DeviceEventsDicCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            ProcessListOfObservers(TableNames.DeviceEvents, InterpretChangedEventArgs(e, TableNames.DeviceEvents));
        }

        private void DeviceTypesDicCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            ProcessListOfObservers(TableNames.DeviceTypes, InterpretChangedEventArgs(e, TableNames.DeviceTypes));
        }

        private DataItemControllerChangedEventArgs InterpretChangedEventArgs(NotifyCollectionChangedEventArgs e, string TableName)
        {
            var Change = new DataItemControllerChangedEventArgs() { Action = e.Action, TableName = TableName };
            if ((e.OldItems != null) && (e.OldItems.Count != 0))
                Change.OldController = ((KeyValuePair<int, DataItemController>)e.OldItems[0]).Value;
            if ((e.NewItems != null) && (e.NewItems.Count != 0))
                Change.NewController = ((KeyValuePair<int, DataItemController>)e.NewItems[0]).Value;
            return Change;
        }

        private void ProcessListOfObservers(string listName, DataItemControllerChangedEventArgs e)
        {
            ListsDic[listName].ForEach(observer =>
            {
                PerformActionOnObserver(observer, () => observer.Update(e));
            });
        }

        private void PerformActionOnObserver(IObserver observer, Action action)
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
