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
using MiaMain;
namespace MiaAppInterface
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


        public override void RefreshComboBoxes(object dataContext)
        {
            DevicesCompanyIdComboBox.RefreshDataContext(dataContext);
            DevicesTypeParentComboBox.RefreshDataContext(dataContext);
        }

        public override void RefreshComboBoxes()
        {
            DevicesCompanyIdComboBox.RefreshDataContext(DataContext);
            DevicesTypeParentComboBox.RefreshDataContext(DataContext);
        }

        private void DevicesTypeIdComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DevicesTypeIdComboBox.IsEnabled)
            {
                var device = (Device)DevicesTypeIdComboBox.DataContext;
                var selectedItem = ((DeviceType)DevicesTypeIdComboBox.SelectedItem);
                if ((device != null) && (selectedItem != null))
                    DevicesTypeParentComboBox.RefreshDataContext(DataContext);
            }
        }
    }
}
