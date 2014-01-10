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
            UpdateClient.SetTimestamp((byte[])DBHelper.PerformDBAction(Connection.GetConnection(), new GetTimestamp()));
            UpdateClient.SetMainWindow(this);
            FactoriesVault.FillFactories(Connection.GetConnection());
            DataItemsListBox.ItemsSource = FactoriesVault.FactoriesDic[FactoryName].GetDataItemsDic();
            var workerThread = new Thread(() => UpdateClient.Control(Connection.GetConnection().ConnectionString));
            workerThread.Start();
        }
        private void DataItemsListBoxItem_DoubleClick (object sender, MouseButtonEventArgs e)
        {
            var company = ((KeyValuePair<int, DataItem>)(sender as ListBoxItem).Content).Value as Company;
            DBHelper.PerformDBAction(Connection.GetConnection(), new FillDataItem(company, FactoriesVault.FactoriesDic[FactoryName], FactoriesVault.FactoriesDic[FactoryName].OtherTableFields));
            var tabItem = DataItemsTabControl.GetTabItemByDataContext(company.Id);
            if (tabItem == null)
                DataItemsTabControl.Items.Add(new DeviceTabItem(company, FactoriesVault.FactoriesDic[FactoryName]) { DataContext = company, IsSelected = true });
            else
                tabItem.IsSelected = true;
        }

        private void Insert_Click(object sender, RoutedEventArgs e)
        {
            var companiesFactory = new CompaniesFactory();
            var company = (Company)companiesFactory.GetDataItem();
            company.Name = "New Item";
            company.Info = "New";
            DBHelper.PerformDBAction(Connection.GetConnection(), new GetNewDataItemId(companiesFactory, company));
            DBHelper.PerformDBAction(Connection.GetConnection(), new InsertDataItem(companiesFactory, company));
            DataItemsTabControl.Items.Add(new DeviceTabItem(company, FactoriesVault.FactoriesDic[FactoryName]) { DataContext = company, IsSelected = true });
        }
    }
}
