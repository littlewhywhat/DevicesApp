using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using InterfaceToDataBase;
namespace InterfaceToClient
{
    /// <summary>
    /// Логика взаимодействия для DevicesGrid.xaml
    /// </summary>
    public partial class DevicesInfoGrid : DataItemsInfoGrid
    {
        public DevicesInfoGrid()
        {
            InitializeComponent();
            
        }


        private void DevicesTypeIdComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (DevicesTypeIdComboBox.IsEnabled)
            //{
            //    ((DataContextControl<DataItemController>)DevicesTypeParentComboBox.DataContext).Refresh();
            //}
        }

        private void DevicesTypeParentComboBox_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }

        private void DataItemsInfoGrid_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }

        private void DeviceProductNumber_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
