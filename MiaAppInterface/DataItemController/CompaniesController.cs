using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiaMain;
using System.Windows.Controls;

namespace MiaAppInterface
{
    public class CompaniesController : DataItemsController
    {
        public CompaniesController() : base(TableNames.Companies)
        { }

        protected override Grid GetTabItemContent()
        {
            var dataGrid = new TabItemGrid();
            dataGrid.ContentGrid = new CompaniesInfoGrid();
            return dataGrid;
        }

        public override DataItem GetNewDataItem()
        {
            var dataItem = Factory.GetEmptyDataItem();
            dataItem.Name = "New item";
            return dataItem;
        }
    }
}
