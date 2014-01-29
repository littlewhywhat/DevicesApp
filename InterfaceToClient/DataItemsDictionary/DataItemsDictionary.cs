using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InterfaceToDataBase;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

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

        public virtual List<DataItemController> GetPossibleParents(DataItemController dataItemController)
        {
            var result = DataItemControllersDic.Values.Where(controller => controller.Id != dataItemController.Id).ToList();
            if (!dataItemController.HasParents)
                return result;
            return FilterPossibleRecursion(result, dataItemController.Id);
        }

        private List<DataItemController> FilterPossibleRecursion(List<DataItemController> parentsList, int id)
        {
            for (int i = 0; i < parentsList.Count; i++)
            {
                var item = parentsList[i];
                if (item.HasTheSameParentId(id))
                {
                    FilterPossibleRecursion(parentsList, item.Id);
                    i = parentsList.IndexOf(item) - 1;
                    parentsList.Remove(item);
                }
            }
            return parentsList;
        }

        public IEnumerable<DataItemController> GetDevicesWithoutParents()
        {
            return GetChildrenByParentId(0);
        }

        public IEnumerable<DataItemController> GetChildrenByParentId(int Id)
        {
            return DataItemControllersDic.Values.Where(dataItemController => dataItemController.HasTheSameParentId(Id));
        }

        public IEnumerable<SearchResult> Search(List<string> searchList)
        {
            return DataItemControllersDic.Values.Select(dataItemController => new SearchResult(dataItemController.Search(searchList), dataItemController));
        }

    }
}
