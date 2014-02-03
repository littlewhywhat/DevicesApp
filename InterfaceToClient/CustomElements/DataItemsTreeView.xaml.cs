using DataItemsLibrary;
using System;
using System.Linq;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Collections.Specialized;
using DataItemControllersLibrary;

namespace InterfaceToClient
{
    /// <summary>
    /// Логика взаимодействия для DataItemsTreeView.xaml
    /// </summary>
    public partial class DataItemsTreeView : TreeView, IObserver
    {
        private DataItemControllersWithParentsDictionary Dictionary;
        public DataItemsTreeView()
        {
            InitializeComponent();
        }
        public void BuildTree(DataItemControllersWithParentsDictionary dictionary)
        {
            Items.Clear();
            Dispose();
            Dictionary = dictionary;
            Vault.ChangesGetter.AddObserver(this, new string[] { Dictionary.Factory.TableName });
            Dictionary.GetDevicesWithoutParents().ToList().ForEach(dataItemController =>
                {
                    AddTreeViewItemByDataItemController(dataItemController);
                });
        }

        public List<DataItemsTreeViewItem> GetNodesFromDicWithParentId(DataItemControllerWithParents dataItemController)
        {
            return Dictionary.GetChildrenByParentId(dataItemController.Id).Select(controller => new DataItemsTreeViewItem() { DataContext = controller }).ToList();
        }

        private DataItemsTreeViewItem AddTreeViewItemByDataItemController(DataItemControllerWithParents dataItemController)
        {
            var treeViewItemParent = FindTreeViewItemById(dataItemController.HasParents? dataItemController.Parent.Id : 0);
            if ((treeViewItemParent != null) && ((treeViewItemParent is TreeView) || ((treeViewItemParent.Parent is TreeView) 
                || (((TreeViewItem)treeViewItemParent.Parent).IsExpanded))))
            {
                var treeViewItemNew = new DataItemsTreeViewItem() { DataContext = dataItemController };
                treeViewItemParent.Items.Add(treeViewItemNew);
                if ((treeViewItemParent is TreeView) || ((TreeViewItem)treeViewItemParent).IsExpanded)
                    GetNodesFromDicWithParentId(dataItemController).ForEach(item => treeViewItemNew.Items.Add(item));
                return treeViewItemNew;
            }
            return null;
        }
        
        private void RemoveTreeViewItem(ItemsControl treeViewItem)
        {
            if (treeViewItem != null)
                ((ItemsControl)treeViewItem.Parent).Items.Remove(treeViewItem);
        }
        
        private ItemsControl FindTreeViewItemById(int id)
        {
            if (id == 0)
                return this;
            else
                return FindTreeViewItemById(id, this);
        }

        private ItemsControl FindTreeViewItemById(int id, ItemsControl control)
        {
            foreach (ItemsControl item in control.Items)
            {
                var dataItemController = (DataItemController)item.DataContext;
                if (dataItemController.Id == id)
                    return item;
                var result = FindTreeViewItemById(id, item);
                if (result != null)
                    return result;
            }
            return null;
        }

        public void Update(DataItemControllerChangedEventArgs Change)
        {
            try
            {
                switch (Change.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                        Add(Change.NewController);
                        break;
                    case NotifyCollectionChangedAction.Remove:
                        Remove(Change.OldController);
                        break;
                    case NotifyCollectionChangedAction.Replace:
                        Replace(Change.NewController, Change.OldController);
                        break;
                }
            }
            catch (Exception)
            { }
        }

        public void Add(DataItemController dataItemController)
        {
            AddTreeViewItemByDataItemController((DataItemControllerWithParents)dataItemController);
        }

        public void Remove(DataItemController dataItemController)
        {
            RemoveTreeViewItem(FindTreeViewItemById(dataItemController.Id));
            Dictionary.GetDevicesWithoutParents().Where(controller => FindTreeViewItemById(controller.Id) == null)
                .ToList().ForEach(controller => AddTreeViewItemByDataItemController(controller));
        }

        public void Replace(DataItemController dataItemControllerNew, DataItemController dataItemControllerOld)
        {
            var dataItemControllerWithParentNew = (DataItemControllerWithParents)dataItemControllerNew;
            var dataItemControllerWithParentOld = (DataItemControllerWithParents)dataItemControllerOld;

            var treeViewItem = FindTreeViewItemById(dataItemControllerOld.Id);
            if (dataItemControllerWithParentNew.Parent != dataItemControllerWithParentOld.Parent)
            {
                RemoveTreeViewItem(treeViewItem);
                treeViewItem = AddTreeViewItemByDataItemController(dataItemControllerWithParentNew);
            }
            if (treeViewItem != null)
                treeViewItem.DataContext = dataItemControllerNew;
        }


        public void Dispose()
        {
            Vault.ChangesGetter.RemoveObserver(this);
        }
    }
}
