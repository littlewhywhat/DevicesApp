using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataItemControllersLibrary;
using System.Windows;
using System.Windows.Controls;

namespace InterfaceToClient
{
    public class ExtCompanyControllersFactory : CompanyControllersFactory, IExtDataItemControllersFactory
    {
        public ExtCompanyControllersFactory(DictionariesVault vault) : base(vault) { }

        public DataItemsInfoGrid GetDataItemsInfoGrid(DataItemController controller)
        {
            return new CompaniesInfoGrid();
        }

        public DataItemsTabItem GetTabItem(DataItemController controller)
        {
            return controller.GetTabItemDefault();
        }

        public FrameworkElement GetPanel(DataItemController controller)
        {
            return new CompanyPanel();
        }

        public Grid GetTabItemContent(DataItemController controller)
        {
            return controller.GetTabItemContentDefault();
        }
    }
}
