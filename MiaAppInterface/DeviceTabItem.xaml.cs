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
    /// Логика взаимодействия для UserControl1.xaml
    /// </summary>
    public partial class DeviceTabItem : TabItem
    {
        public DeviceTabItem(DataItem dataItem)
        {
            InitializeComponent();
            DataContext = dataItem;
            dataItem.Factory.GetDataItemsDic().CollectionChanged += DeviceTabItem_CollectionChanged;
        }

        void DeviceTabItem_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            CompanyParentComboBox.DataContext = null;
            CompanyParentComboBox.DataContext = DataContext;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            var tabControl = (TabControl)this.Parent;
            tabControl.Items.Remove(this);
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            var company = DataContext as Company;
            var companyNew = company.Factory.GetDataItem() as Company;
            companyNew.Id = company.Id;
            companyNew.Info = CompanyInfo.Text;
            companyNew.Name = CompanyName.Text;
            companyNew.ParentId = ((DataItem)CompanyParentComboBox.SelectedItem).Id;
            companyNew.Update();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var company = DataContext as Company;
            company.Delete();
            this.Close_Click(sender, e);
        }

    }
}
