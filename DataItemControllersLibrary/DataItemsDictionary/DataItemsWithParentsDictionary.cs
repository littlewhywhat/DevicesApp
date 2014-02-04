using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataItemControllersLibrary
{
    public abstract class DataItemControllersWithParentsDictionary : DataItemControllersDictionary
    {
        public DataItemControllersWithParentsDictionary(DictionariesVault vault) : base(vault) { }
        public DataItemControllersWithParentsDictionary(DataItemControllersFactory factory) : base(factory) { }

        const string _WithoutParent = "Без родителя";
        private DataItemControllerWithParents WithoutParentController;
        private DataItemControllerWithParents InitWithoutParentController()
        {
            var controller = Factory.GetControllerEmpty();
            controller.Name = _WithoutParent;
            return (DataItemControllerWithParents)controller;
        }
        private List<DataItemControllerWithParents> FilterPossibleRecursion(List<DataItemControllerWithParents> parentsList, int id)
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

        internal DataItemControllerWithParents GetWithoutParentController()
        {
            if (WithoutParentController == null)
                WithoutParentController = InitWithoutParentController();
            return WithoutParentController;
        }
        internal DataItemControllerWithParents GetParentById(int id)
        {
            return (DataItemControllerWithParents)DataItemControllersDic[id];
        }      
        internal virtual IEnumerable<DataItemControllerWithParents> GetPossibleParents(DataItemControllerWithParents dataItemController)
        {
            var result = DataItemControllersDic.Values.Where(controller => controller.Id != dataItemController.Id)
                .Cast<DataItemControllerWithParents>();
            if (dataItemController.InsertMode)
                return result;
            return FilterPossibleRecursion(result.Cast<DataItemControllerWithParents>().ToList(), dataItemController.Id);
        }

        public IEnumerable<DataItemControllerWithParents> GetDevicesWithoutParents()
        {
            return DataItemControllersDic.Values.Cast<DataItemControllerWithParents>().Where(dataItemController => !dataItemController.HasParents);
        }
        public IEnumerable<DataItemControllerWithParents> GetChildrenByParentId(int Id)
        {
            return DataItemControllersDic.Values.Cast<DataItemControllerWithParents>().Where(dataItemController => dataItemController.IsChildOf(Id));
        }



    }
}
