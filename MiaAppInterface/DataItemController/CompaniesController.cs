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
        const string FactoryName = "Companies";
        public CompaniesController() : base(FactoryName)
        { }

        protected override Grid GetTabItemContent()
        {
            var dataGrid = new ManagerGrid();
            dataGrid.contentGrid.Children.Add(new CompaniesInfoGrid());
            return dataGrid;
        }
    }
}
