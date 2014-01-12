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
using System.Data.SqlClient;
using System.Threading;
using MiaMain;
using System.Collections.ObjectModel;
namespace MiaAppInterface
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const string FactoryName = "Companies";
        
        public MainWindow()
        {
            Initialize();
        }
        public void Initialize()
        {
            InitializeComponent();
            UpdateClient.SetTimestamp(Connection.GetLogTimestamp());
            UpdateClient.SetMainWindow(this);
            FactoriesVault.FillFactories();
            CompaniesTree.BuildTree(FactoriesVault.FactoriesDic[FactoryName]);
            var workerThread = new Thread(() => UpdateClient.Control(Connection.GetConnection().ConnectionString));
            workerThread.Start();
        }
        private void DataItemsListBoxItem_DoubleClick (object sender, MouseButtonEventArgs e)
        {
            var company = ((KeyValuePair<int, DataItem>)(sender as ListBoxItem).Content).Value as Company;
            company.Fill(company.Factory.OtherTableFields);
            var tabItem = DataItemsTabControl.GetTabItemByDataContext(company.Id);
            if (tabItem == null)
                DataItemsTabControl.Items.Add(new DeviceTabItem() { DataContext = company, IsSelected = true });
            else
                tabItem.IsSelected = true;
        }
        private void DataItemsTreeView_DoubleClick (object sender, MouseButtonEventArgs e)
        {
            if (CompaniesTree.SelectedItem != null)
            {
                var company = (DataItem)(CompaniesTree.SelectedItem as TreeViewItem).DataContext as Company;
                company.Fill(company.Factory.OtherTableFields);
                var tabItem = DataItemsTabControl.GetTabItemByDataContext(company.Id);
                if (tabItem == null)
                    DataItemsTabControl.Items.Add(new DeviceTabItem() { DataContext = company, IsSelected = true });
                else
                    tabItem.IsSelected = true;
                e.Handled = true;
            }   
        }

        private void Insert_Click(object sender, RoutedEventArgs e)
        {
            var companiesFactory = new CompaniesFactory();
            var company = (Company)companiesFactory.GetDataItem();
            company.Name = "New Item";
            company.Info = "New";
            company.Insert();
            DataItemsTabControl.Items.Add(new DeviceTabItem() { DataContext = company, IsSelected = true });
        }
    }
}
