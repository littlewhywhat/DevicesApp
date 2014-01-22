using MiaMain;
using System.Linq;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace MiaAppInterface
{
    /// <summary>
    /// Логика взаимодействия для DataItemsTreeView.xaml
    /// </summary>
    public partial class DataItemsTreeView : TreeView, Observer
    {
        private ObservableDictionary<int, DataItem> DataItemsDic;
        public DataItemsTreeView()
        {
            InitializeComponent();
        }
        public void BuildTree(DataItemsController controller)
        {
            Items.Clear();
            FactoriesVault.ChangesGetter.RemoveObserver(this);
            DataItemsDic = controller.Factory.GetDataItemsDic();
            FactoriesVault.ChangesGetter.AddObserver(this, new string[] { controller.Factory.TableName });
            DataItemsDic.Where(dataItem => dataItem.Value.ParentId == 0).ForEach(keyValuePair =>
                {
                    AddTreeViewItemByDataItem(keyValuePair.Value);
                });
        }

        public List<DataItemsTreeViewItem> GetNodesFromDicWithParentId(int parentId)
        {
            return DataItemsDic.Where(dataItem => dataItem.Value.ParentId == parentId).Select(filteredDataItem => new DataItemsTreeViewItem() { DataContext = filteredDataItem.Value }).ToList();
        }
        
        private DataItemsTreeViewItem AddTreeViewItemByDataItem(DataItem dataItemNew)
        {
            var treeViewItemParent = FindTreeViewItemById(dataItemNew.ParentId);
            if ((treeViewItemParent != null) && ((treeViewItemParent is TreeView) || ((treeViewItemParent.Parent is TreeView) 
                || (((TreeViewItem)treeViewItemParent.Parent).IsExpanded))))
            {
                var treeViewItemNew = new DataItemsTreeViewItem() { DataContext = dataItemNew };
                treeViewItemParent.Items.Add(treeViewItemNew);
                if ((treeViewItemParent is TreeView) || ((TreeViewItem)treeViewItemParent).IsExpanded)
                    GetNodesFromDicWithParentId(dataItemNew.Id).ForEach(item => treeViewItemNew.Items.Add(item));
                return treeViewItemNew;
            }
            return null;
        }
        
        private void RemoveTreeViewItemById(ItemsControl treeViewItem)
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
                var dataItem = (DataItem)item.DataContext;
                if (dataItem.Id == id)
                    return item;
                var result = FindTreeViewItemById(id, item);
                if (result != null)
                    return result;
            }
            return null;
        }

        public void Update(DataItemsChange Change)
        {
            switch (Change.Action)
            {
                case NotifyCollectionChangedAction.Add :
                    Add(Change.NewDataItem);
                    break;
                case NotifyCollectionChangedAction.Remove :
                    Remove(Change.OldDataItem);
                    break;
                case NotifyCollectionChangedAction.Replace:
                    Replace(Change.NewDataItem, Change.OldDataItem);
                    break;
            }
        }

        public void Add(DataItem dataItemNew)
        {
            AddTreeViewItemByDataItem(dataItemNew);
        }

        public void Remove(DataItem dataItemOld)
        {
            RemoveTreeViewItemById(FindTreeViewItemById(dataItemOld.Id));
            var dic = DataItemsDic.Where(keyValuePair => keyValuePair.Value.ParentId == 0 &&
                FindTreeViewItemById(keyValuePair.Value.Id) == null);
            dic.ForEach(keyValuePair =>
            AddTreeViewItemByDataItem(keyValuePair.Value));
        }

        public void Replace(DataItem dataItemNew, DataItem dataItemOld)
        {
            var treeViewItem = FindTreeViewItemById(dataItemOld.Id, this);
            if (dataItemNew.ParentId != dataItemOld.ParentId)
            {
                RemoveTreeViewItemById(treeViewItem);
                treeViewItem = AddTreeViewItemByDataItem(dataItemNew);
            }
            if (treeViewItem != null)
                treeViewItem.DataContext = dataItemNew;
        }
    }
}
