using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InterfaceToDataBase;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace InterfaceToClient
{
    public abstract class DataItemsDictionary
    {
        protected ObservableDictionary<int, DataItem> DataItemsDic = new ObservableDictionary<int, DataItem>();
        protected DataItemsFactory Factory;
        public DataItemsDictionary(DataItemsFactory factory)
        {
            Factory = factory;
            Factory.FillDataItemsDic(DataItemsDic);
        }

        public void SetCollectionChangedHandler(NotifyCollectionChangedEventHandler handler)
        {
            DataItemsDic.CollectionChanged += handler;
        }

        public void AddDataItem(int Id)
        {
            var dataItem = Factory.GetFilledDataItem(Id);
            DataItemsDic.Add(dataItem.Id, dataItem);
        }
        public void RemoveDataItem(int Id)
        {
            DataItemsDic.Remove(Id);
        }
        public void UpdateDataItem(int Id)
        {
            var dataItem = Factory.GetFilledDataItem(Id);
            DataItemsDic[Id] = dataItem;
        }

        internal DataItem GetDataItemById(int Id)
        {
            return DataItemsDic.ContainsKey(Id) ? DataItemsDic[Id] : null;
        }

        public IEnumerable<DataItem> GetChildrenByParentId(int Id)
        {
            return DataItemsDic.Values.Where(dataItem => dataItem.ParentId == Id);
        }
        public IEnumerable<DataItem> GetValues()
        {
            return DataItemsDic.Values;
        }

        public string TableName
        {
            get { return Factory.TableName; }
        }


        internal DataItem GetEmptyDataItem()
        {
            return Factory.GetEmptyDataItem();
        }
    }
}
