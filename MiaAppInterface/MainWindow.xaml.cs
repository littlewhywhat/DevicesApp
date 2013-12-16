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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DBHelperDevices helper = new DBHelperDevices("Data Source=" + "WHYWHAT-PC\\SQLEXPRESS" + "; Integrated Security = SSPI; Initial Catalog=" + "MiaDB");
        public MainWindow()
        {
            Initialize();
        }
        public void Initialize()
        {
            InitializeComponent();
            DataItemsListBox.ItemsSource = helper.GetDataItemsDictionary(new List<string>() { "Id", "Info" });
            DataItemsListBox.SelectedIndex = 1;
        }
        private void DataItemsListBoxItem_DoubleClick (object sender, MouseButtonEventArgs e)
        {
            var device = ((KeyValuePair<int, DataItem>)(sender as ListBoxItem).Content).Value as Device;
            if (!DataItemsTabControl.Contains(device))
                DataItemsTabControl.Items.Add(new DeviceTabItem(device) { DataContext = device, IsSelected = true});
        }
    }
}
