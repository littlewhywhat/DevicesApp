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
        CompaniesFactory devicesFactory;
        
        public MainWindow()
        {
            Initialize();
        }
        public void Initialize()
        {
            InitializeComponent();
            devicesFactory = new CompaniesFactory();
            DBHelper.PerformDBAction(connection, new FillDataDic(devicesFactory));
            DataItemsListBox.ItemsSource = devicesFactory.GetDataItemsDic();
            DataItemsListBox.SelectedIndex = 1;
            
        }
        private void DataItemsListBoxItem_DoubleClick (object sender, MouseButtonEventArgs e)
        {
            var device = ((KeyValuePair<int, DataItem>)(sender as ListBoxItem).Content).Value as Company;
            DBHelper.PerformDBAction(connection, new FillDataItem(device, devicesFactory));
            if (!DataItemsTabControl.Contains(device))
                DataItemsTabControl.Items.Add(new DeviceTabItem(device) { DataContext = device, IsSelected = true});
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            Device device = new Device() { Id = 10, Info = "Device10" };
            devicesFactory.GetDataItemsDic().Add(10, device);
            
        }
    }
}
