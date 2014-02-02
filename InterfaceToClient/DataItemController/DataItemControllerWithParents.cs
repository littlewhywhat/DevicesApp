﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InterfaceToDataBase;

namespace InterfaceToClient
{
    public abstract class DataItemControllerWithParents : DataItemController
    {
        public DataItemControllerWithParents(DataItem dataItem, DataItemControllersFactory factory) : base(dataItem, factory) { }
        public DataItemWithParents DataItemWithParents { get { return (DataItemWithParents)DataItem; } }
        protected DataItemWithParents dataItemWithParents { get { return (DataItemWithParents)dataItem; } }
        protected DataItemWithParents cloneWithParents { get { return (DataItemWithParents)clone; } }

        protected bool ParentWasChanged { get { return ChangeMode ? dataItemWithParents.ParentId != cloneWithParents.ParentId : false; } }

        public DataItemControllersWithParentsDictionary DataItemsWithParentsDic { get { return (DataItemControllersWithParentsDictionary)GetDictionary(); } }

        public override void Refresh()
        {
            if (ChangeMode)
                CheckIds();
            base.Refresh();
        }

        protected const string _ParentOrDefault = "ParentOrDefault";
        protected const string _Parents = "Parents";
        protected override void OnPropertyChanged()
        {
            base.OnPropertyChanged();
            OnPropertyChanged(_ParentOrDefault);
            OnPropertyChanged(_Parents);
        }

        public virtual void CheckIds()
        {
            if (!ParentExist)
                DataItemWithParents.ParentId = 0;
        }
        public bool ParentExist
        {
            get
            {
                try { return GetPossibleParents().ToDictionary(dataItemController => dataItemController.Id).ContainsKey(DataItemWithParents.ParentId); }
                catch { return false; }
            }
        }
        public void RefreshAndCheckIdsAfterDelete(DataItemControllerWithParents OldDataItemController)
        {
            if (ChangeMode)
            {
                if (OldDataItemController.Factory == Factory)
                    if (DataItemWithParents.ParentId == OldDataItemController.Id)
                        Parent = DataItemsWithParentsDic.GetWithoutParentController();
            }

        }

        public bool HasParents { get { return DataItemWithParents.ParentId != 0; } }

        public virtual DataItemControllerWithParents Parent
        {
            get { return HasParents ? DataItemsWithParentsDic.GetParentById(DataItemWithParents.ParentId) : null; }
            set { DataItemWithParents.ParentId = value.Id; }
        }

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

        public bool HasTheSameParentId(int id) { return DataItemWithParents.ParentId == id; }

        public bool IsChildOf(int id) { return DataItemWithParents.ParentId == id; }
        public bool IsChildOf(DataItemController controller) { return IsChildOf(controller.Id); }

        private IEnumerable<DataItemControllerWithParents> GetPossibleParents()
        {
            return DataItemsWithParentsDic.GetPossibleParents(this);
        }

        private IEnumerable<DataItemControllerWithParents> GetChildren()
        {
            return DataItemsWithParentsDic.GetChildrenByParentId(Id);
        }

        public DataItemAction GetActionForDeleteParentReference()
        {
            var clone = (DataItemWithParents)GetNewClone();
            clone.ParentId = 0;
            return new DataItemAction(clone, ActionType.UPDATE);
        }

        public override void Delete()
        {
            DataItem.DeleteWithReferences(GetDeleteReferencesActions());
        }

        public void DeleteReferences()
        {
            GetDeleteReferencesActions().PerformActions();
        }

        protected virtual List<DataItemAction> GetDeleteReferencesActions()
        {
            return GetChildren().Select(child => child.GetActionForDeleteParentReference()).ToList();
        }
    }
}