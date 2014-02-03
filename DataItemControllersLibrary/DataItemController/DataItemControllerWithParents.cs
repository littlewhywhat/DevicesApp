using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataItemsLibrary;
using DBActionLibrary;

namespace DataItemControllersLibrary
{
    public abstract class DataItemControllerWithParents : DataItemController
    {
        public DataItemControllerWithParents(DataItem dataItem, DataItemControllersFactory factory) : base(dataItem, factory) { }

        private IEnumerable<DataItemControllerWithParents> GetPossibleParents()
        {
            return DataItemsWithParentsDic.GetPossibleParents(this);
        }
        private DataItemWithParents DataItemWithParents { get { return (DataItemWithParents)DataItem; } }
        private DataItemWithParents dataItemWithParents { get { return (DataItemWithParents)dataItem; } }
        private DataItemWithParents cloneWithParents { get { return (DataItemWithParents)clone; } }
        private bool ParentExist
        {
            get
            {
                try { return GetPossibleParents().ToDictionary(dataItemController => dataItemController.Id).ContainsKey(DataItemWithParents.ParentId); }
                catch { return false; }
            }
        }
        private DataItemAction GetActionForDeleteParentReference()
        {
            var clone = (DataItemWithParents)GetNewClone();
            clone.ParentId = 0;
            return new DataItemAction(clone, ActionType.UPDATE);
        }

        const string _ParentOrDefault = "ParentOrDefault";
        protected const string _Parents = "Parents";
        protected override void OnPropertyChanged()
        {
            base.OnPropertyChanged();
            OnPropertyChanged(_ParentOrDefault);
            OnPropertyChanged(_Parents);
        }
        protected virtual void CheckIds()
        {
            if (!ParentExist)
                DataItemWithParents.ParentId = 0;
        }
        protected bool ParentWasChanged { get { return ChangeMode ? dataItemWithParents.ParentId != cloneWithParents.ParentId : false; } }
        protected DataItemControllersWithParentsDictionary DataItemsWithParentsDic { get { return (DataItemControllersWithParentsDictionary)Factory.RelatedDic; } }
        protected IEnumerable<DataItemControllerWithParents> GetChildren()
        {
            return DataItemsWithParentsDic.GetChildrenByParentId(Id);
        }
        protected void DeleteReferences()
        {
            GetDeleteReferencesActions().PerformActions();
        }
        protected virtual List<DataItemAction> GetDeleteReferencesActions()
        {
            return GetChildren().Select(child => child.GetActionForDeleteParentReference()).ToList();
        }

        internal bool HasChildren { get { return GetChildren().Count() > 0; } }
        internal bool IsChildOf(int id) { return dataItemWithParents.ParentId == id; }

        public override void Refresh()
        {
            if (ChangeMode)
                CheckIds();
            base.Refresh();
        }
        public override void Delete()
        {
            DataItem.DeleteWithReferences(GetDeleteReferencesActions());
        }
        public virtual DataItemControllerWithParents Parent
        {
            get { return HasParents ? DataItemsWithParentsDic.GetParentById(DataItemWithParents.ParentId) : null; }
            set { DataItemWithParents.ParentId = value.Id; }
        }
        public bool HasParents { get { return DataItemWithParents.ParentId != 0; } }
        public DataItemControllerWithParents ParentOrDefault
        {
            get { return HasParents ? Parent : DataItemsWithParentsDic.GetWithoutParentController(); }
            set { Parent = value; }
        }
        public List<DataItemControllerWithParents> Parents
        {
            get
            {
                var result = GetPossibleParents().ToList();
                result.Insert(0, DataItemsWithParentsDic.GetWithoutParentController());
                if (!result.Contains(ParentOrDefault))
                    result.Add(ParentOrDefault);
                return result;
            }
        }
    }
}
