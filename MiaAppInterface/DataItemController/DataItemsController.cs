using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiaMain;
using System.Windows.Controls;

namespace MiaAppInterface
{
    public abstract class DataItemsController
    {
        public DataItemsFactory Factory { get; private set; }
        public DataItemsController(string factoryName)
        {
            Factory = FactoriesVault.FactoriesDic[factoryName];
        }
        public DataItemsTabItem GetTabItem(DataItem dataItem)
        {
            dataItem.Fill(Factory.OtherTableFields);
            return GetTabItemByDataItem(dataItem);
        }
        protected abstract Grid GetDataItemsGrid(DataItem dataItem);
        private DataItemsTabItem GetTabItemByDataItem(DataItem dataItem)
        {
            var dataGrid = new ManagerGrid();
            dataGrid.contentGrid.Children.Add(GetDataItemsGrid(dataItem));
            var tabItem = new DataItemsTabItem() { DataContext = dataItem, Content = dataGrid };
            FactoriesVault.ChangesGetter.AddObserver(tabItem, new string[] { dataItem.Factory.TableName });
            return tabItem;
        }

        public DataItemsTabItem GetInsertTabItem()
        {
            var dataItem = Factory.GetEmptyDataItem();
            dataItem.Name = "New item";
            var tabItem = GetTabItemByDataItem(dataItem);
            var managerGrid = tabItem.Content as ManagerGrid;
            managerGrid.DataContext = dataItem;

            managerGrid.EnableInsertMode();
            return tabItem;
        }
    }
}
