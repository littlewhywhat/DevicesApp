using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiaMain;

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
        protected abstract DataItemsGrid GetDataItemsGrid();
        private DataItemsTabItem GetTabItemByDataItem(DataItem dataItem)
        {
            var dataGrid = new ManagerGrid();
            dataGrid.contentGrid.Children.Add(GetDataItemsGrid());
            return new DataItemsTabItem(dataItem) { Content = dataGrid }; 
        }
        public DataItemsTabItem GetNewTabItem()
        {
            var dataItem = Factory.GetEmptyDataItem();
            dataItem.Name = "New item";
            return GetTabItemByDataItem(dataItem);
        }
    }
}
