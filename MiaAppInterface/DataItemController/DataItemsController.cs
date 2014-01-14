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
        public abstract DataItemsFactory Factory { get; }
        public DataItemsTabItem GetTabItem(DataItem dataItem)
        {
            dataItem.Fill(Factory.OtherTableFields);
            var dataGrid = new DataGrid();
            dataGrid.contentGrid.Children.Add(GetDataItemsGrid());
            return new DataItemsTabItem(dataItem) { Content = dataGrid }; 
        }
        protected abstract DataItemsGrid GetDataItemsGrid();
        protected abstract DataItem GetNewDataItem();
        public DataItemsTabItem GetTabItem()
        {
            return GetTabItem(GetNewDataItem());
        }
    }
}
