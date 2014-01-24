using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiaMain;
using System.Collections.Specialized;
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
        protected abstract Grid GetTabItemContent();
        private DataItemsTabItem GetTabItemByDataItem(DataItem dataItem)
        {
            var tabItem = new DataItemsTabItem() { DataContext = new DataItemsChange() { NewDataItem = dataItem }, Content = GetTabItemContent() };
            FactoriesVault.ChangesGetter.AddObserver(tabItem, new string[] { dataItem.Factory.TableName });
            return tabItem;
        }

        public abstract DataItem GetNewDataItem();
        

        public DataItemsTabItem GetInsertTabItem()
        {

            //var managerGrid = tabItem.Content as ManagerGrid;
            //managerGrid.DataContext = new DataItemsChange() { NewDataItem = dataItem };

            //managerGrid.EnableInsertMode();
            return GetTabItemByDataItem(GetNewDataItem());
        }
    }
}
