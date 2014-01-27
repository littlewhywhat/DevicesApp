using InterfaceToDataBase;
using System;
using System.Linq;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace InterfaceToClient
{
    /// <summary>
    /// Логика взаимодействия для DataItemsTreeView.xaml
    /// </summary>
    public partial class DataItemsTreeView : TreeView, Observer
    {
        private DataItemControllersFactory Factory;
        public DataItemsTreeView()
        {
            InitializeComponent();
        }
        public void BuildTree(DataItemControllersFactory factory)
        {
            Items.Clear();
            Dispose();
            Factory = factory;
            FactoriesVault.ChangesGetter.AddObserver(this, new string[] { Factory.TableName });
            Factory.GetChildren(Factory.GetEmptyController()).ForEach(dataItemController =>
                {
                    AddTreeViewItemByDataItemController(dataItemController);
                });
        }

        public List<DataItemsTreeViewItem> GetNodesFromDicWithParentId(DataItemController dataItemController)
        {
            return Factory.GetChildren(dataItemController).Select(controller => new DataItemsTreeViewItem() { DataContext = controller }).ToList();
        }

        private DataItemsTreeViewItem AddTreeViewItemByDataItemController(DataItemController dataItemController)
        {
            var treeViewItemParent = FindTreeViewItemById(dataItemController.Parent.Id);
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
            AddTreeViewItemByDataItemController(dataItemController);
        }

        public void Remove(DataItemController dataItemController)
        {
            RemoveTreeViewItem(FindTreeViewItemById(dataItemController.Id));
            Factory.DataItemsDic.GetChildrenByParentId(0).Where(dataItem => FindTreeViewItemById(dataItem.Id) == null)
                .Select(dataItem => Factory.GetController(dataItem)).ToList().ForEach(controller =>
                    AddTreeViewItemByDataItemController(controller));
        }

        public void Replace(DataItemController dataItemControllerNew, DataItemController dataItemControllerOld)
        {
            var treeViewItem = FindTreeViewItemById(dataItemControllerOld.Id);
            if (dataItemControllerNew.Parent.Id != dataItemControllerOld.Parent.Id)
            {
                RemoveTreeViewItem(treeViewItem);
                treeViewItem = AddTreeViewItemByDataItemController(dataItemControllerNew);
            }
            if (treeViewItem != null)
                treeViewItem.DataContext = dataItemControllerNew;
        }


        public void Dispose()
        {
            FactoriesVault.ChangesGetter.RemoveObserver(this);
        }
    }
}
