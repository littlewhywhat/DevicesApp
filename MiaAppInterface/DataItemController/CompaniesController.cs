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
        protected override DataItemsGrid GetDataItemsGrid()
        {
            return new CompaniesGrid();
        }
        
        protected override DataItem GetNewDataItem()
        {
            var company = (Company)factory.GetDataItem();
            company.Name = "New Item";
            company.Info = "New";
            company.Insert();
            return company;
        }
    }
}
