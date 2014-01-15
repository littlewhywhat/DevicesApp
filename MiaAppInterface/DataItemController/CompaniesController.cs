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
        public CompaniesController() : base(FactoryName)
        { }
        protected override DataItemsGrid GetDataItemsGrid()
        {
            return new CompaniesGrid(this);
        }
        protected override void GetDataItemToUpdate(DataItem dataItem, DataItemsGrid grid)
        {
            var companyNew = dataItem as Company;
            var companiesGrid = grid as CompaniesGrid;
            companyNew.Info = companiesGrid.CompanyInfo.Text;
            companyNew.Name = companiesGrid.CompanyName.Text;
            var selectedItem = ((Company)companiesGrid.CompanyParentComboBox.SelectedItem);
            companyNew.ParentId = selectedItem == null ? 0 : selectedItem.Id;
        }

        protected override DataItem GetNewDataItem()
        {
            var company = (Company)Factory.GetDataItem();
            company.Name = "New Item";
            company.Info = "New";
            company.Insert();
            return company;
        }
    }
}
