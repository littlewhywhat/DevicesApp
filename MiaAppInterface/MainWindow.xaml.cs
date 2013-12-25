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
        SqlConnection connection = new SqlConnection("Data Source=" + "WHYWHAT-PC\\SQLEXPRESS" + "; Integrated Security = SSPI; Initial Catalog=" + "MiaDB");
        CompaniesFactory companiesFactory;
        
        public MainWindow()
        {
            Initialize();
        }
        public void Initialize()
        {
            InitializeComponent();
            UpdateClient.SetTimestamp((byte[])DBHelper.PerformDBAction(connection, new GetTimestamp()));
            FactoriesVault.FillFactories(connection);
            companiesFactory = (CompaniesFactory)FactoriesVault.FactoriesDic["Companies"];
            DataItemsListBox.ItemsSource = companiesFactory.GetDataItemsDic();
            var workerThread = new Thread(() => UpdateClient.Control(connection.ConnectionString));
            workerThread.Start();
            
            
        }
        private void DataItemsListBoxItem_DoubleClick (object sender, MouseButtonEventArgs e)
        {
            var device = ((KeyValuePair<int, DataItem>)(sender as ListBoxItem).Content).Value as Company;
            DBHelper.PerformDBAction(connection, new FillDataItem(device, companiesFactory));
            if (!DataItemsTabControl.Contains(device))
                DataItemsTabControl.Items.Add(new DeviceTabItem(device) { DataContext = device, IsSelected = true});
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            Device device = new Device() { Id = 10, Info = "Device10" };
            companiesFactory.GetDataItemsDic().Add(10, device);
            
        }
    }
}
