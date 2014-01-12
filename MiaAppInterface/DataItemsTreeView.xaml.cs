using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using MiaMain;
using System.Collections.ObjectModel;

namespace MiaAppInterface
{
    /// <summary>
    /// Логика взаимодействия для DataItemsTreeView.xaml
    /// </summary>
    public partial class DataItemsTreeView : TreeView
    {
        ObservableDictionary<int, DataItem> DataItemsDic;
        public DataItemsTreeView()
        {
            InitializeComponent();
        }
        public void BuildTree(DataItemsFactory factory)
        {
            Items.Clear();
            DataItemsDic = factory.GetDataItemsDic();
            DataItemsDic.CollectionChanged += DataItemsDic_CollectionChanged;
            GetNodesFromDicWithParentId(0).ForEach(item =>
                {
                    this.AddChild(item);
                    GetNodesFromDicWithParentId(((DataItem)item.DataContext).Id).ForEach(child => item.Items.Add(child));
                });
        }
        private void DataItemsDic_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    var dataItemRemove = ((KeyValuePair<int, DataItem>)e.OldItems[0]).Value;
                    RemoveTreeViewItemById(FindTreeViewItemById(dataItemRemove.Id));
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Replace:
                    var dataItemNew = ((KeyValuePair<int, DataItem>)e.NewItems[0]).Value;
                    var dataItemOld = ((KeyValuePair<int, DataItem>)e.OldItems[0]).Value;
                    var treeViewItem = FindTreeViewItemById(dataItemOld.Id, this);
                    if (dataItemNew.ParentId != dataItemOld.ParentId)
                    {
                        RemoveTreeViewItemById(treeViewItem);
                        treeViewItem = AddTreeViewItemByDataItem(dataItemNew);
                    }
                    if (treeViewItem != null)
                        treeViewItem.DataContext = dataItemNew;
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    var dataItemAdd = ((KeyValuePair<int, DataItem>)e.NewItems[0]).Value;
                    AddTreeViewItemByDataItem(dataItemAdd);
                    break;
            }
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
    }
}
