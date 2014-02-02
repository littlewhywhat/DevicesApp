using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InterfaceToDataBase;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;

namespace InterfaceToClient
{
    public abstract class DataItemControllersDictionary : IDataItemDic
    {
        
        public DataItemControllersFactory Factory;
        public ObservableDictionary<int, DataItemController> DataItemControllersDic = new ObservableDictionary<int, DataItemController>();
        public DataItemControllersDictionary()
        {
            Factory = GetFactory();
            Factory.FillDataItemControllersDic(this);
        }
        protected abstract DataItemControllersFactory GetFactory();
        public void AddDataItem(DataItem dataItem)
        {
 	        DataItemControllersDic.Add(dataItem.Id, Factory.GetController(dataItem));
        }
        public void AddDataItem(int id)
        {
            DataItemControllersDic.Add(id, Factory.GetController(id));
        }
        public void UpdateDataItem(int id)
        {
            DataItemControllersDic[id] = Factory.GetController(id);
        }
        public void RemoveDataItem(int id)
        {
            DataItemControllersDic.Remove(id);
        }
        public DataItemController GetDataItemControllerById(int Id)
        {
            return DataItemControllersDic.ContainsKey(Id) ? DataItemControllersDic[Id] : null;
        }

       

        public IEnumerable<FrameworkElement> Search(List<string> searchList)
        {
            return DataItemControllersDic.Values.Select(dataItemController =>
                new SearchResult(dataItemController.Search(searchList), dataItemController)).Where(searchResult => searchResult.Result != null).Select(searchResult =>
                    {
                        var panel = searchResult.Reference.GetPanel();
                        panel.Tag = searchResult.Result;
                        return panel;
                    });
        }

    }
}
