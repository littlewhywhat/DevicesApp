using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiaMain;

namespace MiaAppInterface
{
    public class CompaniesController : DataItemsController
    {
        const string FactoryName = "Companies";
        private  DataItemsFactory factory;
        public override DataItemsFactory Factory { get { return factory; } }
        public CompaniesController()
        {
            factory = FactoriesVault.FactoriesDic[FactoryName];
        }
        public override DataItemsTabItem GetTabItem(DataItem dataItem)
        {
            dataItem.Fill(Factory.OtherTableFields);
            var dataGrid = new DataGrid();
            dataGrid.contentGrid.Children.Add(new CompaniesGrid());
            return new DataItemsTabItem(dataItem) { Content = dataGrid };
        }
        protected override DataItem GetNewDataItem()
        {
            var company = (Company)factory.GetDataItem();
            company.Name = "New Item";
            company.Info = "New";
            company.Insert();
            return company;
        }
        public override DataItemsTabItem GetTabItem()
        {
            return GetTabItem(GetNewDataItem());
        }
    }
}
