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
    /// Логика взаимодействия для DataGrid.xaml
    /// </summary>
    public partial class DataGrid : Grid
    {
        public DataGrid()
        {
            InitializeComponent();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var dataItem = DataContext as DataItem;
            dataItem.Delete();
            var tabItem = this.Parent as DataItemsTabItem;
            tabItem.CloseTabItem();
            e.Handled = true;
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            var dataItemGrid = contentGrid.Children[0] as DataItemsGrid;
            dataItemGrid.UpdateDataItem();
        }
    }
}
