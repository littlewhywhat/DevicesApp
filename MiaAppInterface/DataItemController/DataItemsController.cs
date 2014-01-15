using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            var dataGrid = new DataGrid();
            dataGrid.contentGrid.Children.Add(GetDataItemsGrid());
            return new DataItemsTabItem(dataItem) { Content = dataGrid }; 
        }
        protected abstract DataItemsGrid GetDataItemsGrid();
        protected abstract DataItem GetNewDataItem();
        public void UpdateDataItem(DataItem dataItem, DataItemsGrid grid)
        {
            var dataItemNew = Factory.GetDataItem();
            dataItemNew.Id = dataItem.Id;
            GetDataItemToUpdate(dataItemNew, grid);
            dataItemNew.Update();
        }
        protected abstract void GetDataItemToUpdate(DataItem dataItem, DataItemsGrid grid);
        public DataItemsTabItem GetTabItem()
        {
            return GetTabItem(GetNewDataItem());
        }
    }
}
