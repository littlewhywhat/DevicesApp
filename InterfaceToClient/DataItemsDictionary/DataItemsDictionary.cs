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
        public DataItemController WithoutParentController;
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
            if (dataItemController.InsertMode)
                return result;
            return FilterPossibleRecursion(result, dataItemController.Id);
        }

        private List<DataItemController> FilterPossibleRecursion(List<DataItemController> parentsList, int id)
        {
            var filterStack = new Stack<int>();
            filterStack.Push(id);
            while (filterStack.Count != 0)
            {
                var parentId = filterStack.Pop();
                for (var i = 0; i < parentsList.Count; i++)
                {
                    var item = parentsList[i];
                    if (item.IsChildOf(parentId))
                    {
                        parentsList.Remove(item);
                        filterStack.Push(item.Id);
                        i--;
                    }
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

        const string _WithoutParent = "Без родителя";
        private DataItemController InitWithoutParentController()
        {
            var controller = Factory.GetControllerEmpty();
            controller.Name = _WithoutParent;
            return controller;
        }

        public DataItemController GetWithoutParentController()
        {
            if (WithoutParentController == null)
                WithoutParentController = InitWithoutParentController();
            return WithoutParentController;
        }

    }
}
