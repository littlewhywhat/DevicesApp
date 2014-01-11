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
        public DeviceTabItem()
        {
            InitializeComponent();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            var tabControl = (TabControl)this.Parent;
            tabControl.Items.Remove(this);
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            var company = DataContext as Company;
            company.Info = CompanyInfo.Text;
            company.Name = CompanyName.Text;
            company.Update();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var company = DataContext as Company;
            company.Delete();
            this.Close_Click(sender, e);
        }
    }
}
