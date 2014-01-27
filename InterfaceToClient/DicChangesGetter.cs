using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;
using System.Collections.Specialized;
using System.Windows;
using InterfaceToDataBase;

namespace InterfaceToClient
{
    public class DicChangesGetter
    {
        Dictionary<string, List<Observer>> ListsDic = new Dictionary<string, List<Observer>>();
        public DicChangesGetter()
        {
            FactoriesVault.Dic[TableNames.Devices].SetCollectionChangedHandler(DevicesDicCollectionChanged);
            ListsDic.Add(TableNames.Devices, new List<Observer>());
            FactoriesVault.Dic[TableNames.DeviceTypes].SetCollectionChangedHandler(DeviceTypesDicCollectionChanged);
            ListsDic.Add(TableNames.DeviceTypes, new List<Observer>());
            FactoriesVault.Dic[TableNames.DeviceEvents].SetCollectionChangedHandler(DeviceEventsDicCollectionChanged);
            ListsDic.Add(TableNames.DeviceEvents, new List<Observer>());
            FactoriesVault.Dic[TableNames.Companies].SetCollectionChangedHandler(CompaniesDicCollectionChanged);
            ListsDic.Add(TableNames.Companies, new List<Observer>());
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

        private void DevicesDicCollectionChanged(object sender, DataItemControllerChangedEventArgs e)
        {
            ProcessListOfObservers(TableNames.Devices, e);
        }

        private void CompaniesDicCollectionChanged(object sender, DataItemControllerChangedEventArgs e)
        {
            ProcessListOfObservers(TableNames.Companies, e);
        }

        private void DeviceEventsDicCollectionChanged(object sender, DataItemControllerChangedEventArgs e)
        {
            ProcessListOfObservers(TableNames.DeviceEvents, e);
        }

        private void DeviceTypesDicCollectionChanged(object sender, DataItemControllerChangedEventArgs e)
        {
            ProcessListOfObservers(TableNames.DeviceTypes, e);
        }

        private void ProcessListOfObservers(string listName, DataItemControllerChangedEventArgs e)
        {
            ListsDic[listName].ForEach(observer =>
            {
                PerformActionOnObserver(observer, () => observer.Update(e));
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
