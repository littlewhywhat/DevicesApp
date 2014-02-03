using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataItemControllersLibrary
{
    public abstract class DataItemControllersWithParentsDictionary : DataItemControllersDictionary
    {
        public DataItemControllerWithParents WithoutParentController;
        public DataItemControllersWithParentsDictionary(DictionariesVault vault) : base(vault) { }
        public DataItemControllersWithParentsDictionary(DataItemControllersFactory factory) : base(factory) { }

        public virtual IEnumerable<DataItemControllerWithParents> GetPossibleParents(DataItemControllerWithParents dataItemController)
        {
            var result = DataItemControllersDic.Values.Where(controller => controller.Id != dataItemController.Id)
                .Cast<DataItemControllerWithParents>();
            if (dataItemController.InsertMode)
                return result;
            return FilterPossibleRecursion(result.Cast<DataItemControllerWithParents>().ToList(), dataItemController.Id);
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

        public IEnumerable<DataItemControllerWithParents> GetDevicesWithoutParents()
        {
            return GetChildrenByParentId(0);
        }

        public IEnumerable<DataItemControllerWithParents> GetChildrenByParentId(int Id)
        {
            return DataItemControllersDic.Values.Cast<DataItemControllerWithParents>().Where(dataItemController => dataItemController.IsChildOf(Id));
        }

        const string _WithoutParent = "Без родителя";
        private DataItemControllerWithParents InitWithoutParentController()
        {
            var controller = Factory.GetControllerEmpty();
            controller.Name = _WithoutParent;
            return (DataItemControllerWithParents)controller;
        }

        public DataItemControllerWithParents GetWithoutParentController()
        {
            if (WithoutParentController == null)
                WithoutParentController = InitWithoutParentController();
            return WithoutParentController;
        }

        public DataItemControllerWithParents GetParentById(int id)
        {
            return (DataItemControllerWithParents)DataItemControllersDic[id];
        }

    }
}
