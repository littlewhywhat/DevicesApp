using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using InterfaceToDataBase;
using System.Windows;
using System.Windows.Controls;

namespace InterfaceToClient
{
    public abstract class DataItemControllersFactory
    {

        protected DataItemsFactory Factory;
        public DataItemControllersFactory()
        {
            Factory = GetFactory();
        }
        protected abstract DataItemsFactory GetFactory();
        public void FillDataItemControllersDic(DataItemControllersDictionary dataItemsDic)
        {
            DBHelper.PerformDBAction(Connection.GetConnection(), new FillDataDic(Factory, dataItemsDic));
        }
        public string TableName { get { return Factory.TableName; } }
        public abstract DataItemController GetController(DataItem dataItem);
        public DataItemController GetController(int id)
        {
            return GetController(Factory.GetFirstFilledDataItem(id));
        }
        public DataItemController GetControllerEmpty()
        {
            return GetController(Factory.GetEmptyDataItem());
        }
        public DataItemController GetControllerDefault()
        {
            return GetController(Factory.GetDataItemDefault());
        }

        protected virtual Grid GetTabItemContent()
        {
            var dataGrid = new TabItemGrid();
            dataGrid.ContentGrid = GetDataItemsInfoGrid();
            return dataGrid;
        }
        protected abstract DataItemsInfoGrid GetDataItemsInfoGrid();

        public virtual DataItemsTabItem GetTabItem(DataItemController dataItemController)
        {
            var tabItem = new DataItemsTabItem() { DataContext =  dataItemController , Content = GetTabItemContent() };
            FactoriesVault.ChangesGetter.AddObserver(tabItem, new string[] { TableName });
            return tabItem;
        }

        public DataItemsTabItem GetInsertTabItem()
        {
            return GetTabItem(GetControllerDefault());
        }

        public ListBoxItemGrid GetListBoxItemGrid(DataItemController dataItemController)
        {
            return new ListBoxItemGrid() { ContentGrid = GetDataItemsInfoGrid(), DataContext = dataItemController };
        }

        internal abstract FrameworkElement GetPanel();
    }
}
