using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MiaMain;

namespace MiaAppInterface
{
    /// <summary>
    /// Логика взаимодействия для CompaniesTabItemContent.xaml
    /// </summary>
    public partial class CompaniesGrid : DataItemsGrid
    {
        public CompaniesGrid()
        {
            InitializeComponent();
        }

        private void CompaniesGrid_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            CompanyParentComboBox.RefreshDataContext(DataContext);
        }
        public override void Update(DataItem dataItem)
        {
            var company = DataContext as Company;
            var companyNew = company.Factory.GetDataItem() as Company;
            companyNew.Id = company.Id;
            companyNew.Info = CompanyInfo.Text;
            companyNew.Name = CompanyName.Text;
            var selectedItem = ((DataItem)CompanyParentComboBox.SelectedItem);
            companyNew.ParentId = selectedItem == null? 0: selectedItem.Id;
            companyNew.Update();
        }
    }
}
