using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;
using System.Collections.Specialized;

namespace MiaMain
{
    public class DicChangesGetter
    {
        Dictionary<string, List<Observer>> ListsDic = new Dictionary<string, List<Observer>>();
        public DicChangesGetter()
        {
            FactoriesVault.FactoriesDic["Companies"].GetDataItemsDic().CollectionChanged += CompaniesDicCollectionChanged;
            ListsDic.Add("Companies", new List<Observer>());
            FactoriesVault.FactoriesDic["Devices"].GetDataItemsDic().CollectionChanged += DevicesDicCollectionChanged;
            ListsDic.Add("Devices", new List<Observer>());
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
            ProcessListOfObservers("Devices", e);
        }

        private void CompaniesDicCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            ProcessListOfObservers("Companies", e);
        }

        private void ProcessListOfObservers(string listName, NotifyCollectionChangedEventArgs e)
        {
            
            foreach (var observer in ListsDic[listName])
            {
                DataItem dataItemOld = null;
                DataItem dataItemNew = null;
                if ((e.OldItems != null) && (e.OldItems.Count != 0))
                    dataItemOld = ((KeyValuePair<int, DataItem>)e.OldItems[0]).Value;
                if ((e.NewItems != null) && (e.NewItems.Count != 0))
                    dataItemNew = ((KeyValuePair<int, DataItem>)e.NewItems[0]).Value;
                switch (e.Action)
                {
                    case NotifyCollectionChangedAction.Remove:
                        
                        PerformActionOnObserver(observer, () => observer.Remove(dataItemOld));
                        break;
                    case NotifyCollectionChangedAction.Add:
                        PerformActionOnObserver(observer, () => observer.Add(dataItemNew));
                        break;
                    case NotifyCollectionChangedAction.Replace:
                        PerformActionOnObserver(observer, () => observer.Replace(dataItemNew, dataItemOld));
                        break;
                }
            };
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
