using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataItemsLibrary;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using DBActionLibrary;

namespace DataItemControllersLibrary
{
    public abstract class DataItemControllersDictionary : IDataItemDic
    {
        public DataItemControllersDictionary(DictionariesVault vault)
        {
            Factory = GetFactory(vault);
            Factory.FillDataItemControllersDic(this);
        }
        public DataItemControllersDictionary(DataItemControllersFactory factory)
        {
            Factory = factory;
            Factory.FillDataItemControllersDic(this);
        }
        protected abstract DataItemControllersFactory GetFactory(DictionariesVault vault);

        internal void AddDataItem(int id)
        {
            DataItemControllersDic.Add(id, Factory.GetController(id));
        }
        internal void UpdateDataItem(int id)
        {
            DataItemControllersDic[id] = Factory.GetController(id);
        }
        internal void RemoveDataItem(int id)
        {
            DataItemControllersDic.Remove(id);
        }

        public DataItemControllersFactory Factory;
        public ObservableDictionary<int, DataItemController> DataItemControllersDic = new ObservableDictionary<int, DataItemController>();
        public void AddDataItem(DataItem dataItem)
        {
 	        DataItemControllersDic.Add(dataItem.Id, Factory.GetController(dataItem));
        }
        public IEnumerable<SearchResult> Search(List<string> searchList)
        {
            return DataItemControllersDic.Values.Select(dataItemController =>
                new SearchResult(dataItemController.Search(searchList), dataItemController)).Where(searchResult => searchResult.Result != null);
        }

    }
}
