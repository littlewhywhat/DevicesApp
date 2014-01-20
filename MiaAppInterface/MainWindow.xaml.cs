using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MiaMain;

namespace MiaAppInterface
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataItemsExplorer explorer;
        public MainWindow()
        {
            InitializeComponent();
            FactoriesVault.FillFactories();
            explorer = new DataItemsExplorer() { DataContext = new DevicesController()};
            explorer.Show();
        }

        private void Companies_Click(object sender, RoutedEventArgs e)
        {
            explorer.DataContext = new CompaniesController();
        }

        private void Devices_Click(object sender, RoutedEventArgs e)
        {
            explorer.DataContext = new DevicesController();
        }
        private void ShowExplorer(DataItemsController controller)
        {
            var explorer = new DataItemsExplorer() { DataContext = controller };
            explorer.Show();
            this.Close();
        }
    }
}
