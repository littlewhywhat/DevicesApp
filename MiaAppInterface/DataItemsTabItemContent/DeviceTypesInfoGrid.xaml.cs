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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MiaMain;

namespace MiaAppInterface
{
    /// <summary>
    /// Логика взаимодействия для DeviceTypesInfoGrid.xaml
    /// </summary>
    public partial class DeviceTypesInfoGrid : DataItemsInfoGrid
    {
        public DeviceTypesInfoGrid()
        {
            InitializeComponent();
        }

        public override void RefreshComboBoxes(object dataContext)
        {
            DeviceTypesParentComboBox.RefreshDataContext(dataContext);
        }

        public override void RefreshComboBoxes()
        {
            DeviceTypesParentComboBox.RefreshDataContext(DataContext);
        }

        private void Main_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext != null)
            {
                var type = (DeviceType)DataContext;
                if (type.Id == 0)
                    IsMarker.IsEnabled = true;
                else
                    IsMarker.IsEnabled = false;
                PreviousMarker.Text = ((DeviceType)DataContext).GetMarker();
            }
        }
    }
}
