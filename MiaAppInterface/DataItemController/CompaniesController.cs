using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            return new CompaniesGrid();
        }

    }
}
