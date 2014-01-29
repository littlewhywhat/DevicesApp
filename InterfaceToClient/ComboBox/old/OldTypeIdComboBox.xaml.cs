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
using InterfaceToDataBase;

namespace InterfaceToClient
{
    /// <summary>
    /// Логика взаимодействия для TypeIdComboBox.xaml
    /// </summary>
    public partial class OldTypeIdComboBox : ComboBox, Observer, IDisposable
    {
        DeviceTypesFactory Factory;
        public OldTypeIdComboBox()
        {
            InitializeComponent();
            Items.Clear();
            //Factory = FactoriesVault.FactoriesDic[TableNames.DeviceTypes] as DeviceTypesFactory;
            FactoriesVault.ChangesGetter.AddObserver(this, new string[] { TableNames.DeviceTypes });
        }

        private void ComboBox_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext != null)
            {
                //var device = DataContext as Device;
                //ItemsSource = GetItems(device);
                //if (Factory.DataItemsDic.ContainsKey(device.TypeId))
                //{
                //    SelectedItem = Factory.DataItemsDic[device.TypeId];
                //}
                //else
                //    SelectedIndex = 0;
            }
        }
        private List<DataItem> GetItems(DataItem dataItem)
        {
            //var items = Factory.DataItemsDic.Values.Where(deviceType => !((DeviceType)deviceType).IsMarker).ToList();
            //var emptyItem = Factory.GetEmptyDataItem();
            //emptyItem.Name = "Тип неопределен";
            //items.Insert(0, emptyItem);
            //return items;
            return null;
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // fires when ItemsSource count changed
            if (IsEnabled)
            {
                var device = (Device)DataContext;
                var selectedItem = ((DeviceType)this.SelectedItem);
                if ((device != null) && (selectedItem != null))
                    device.TypeId = selectedItem.Id;
            }
        }

        public void Update(DataItemControllerChangedEventArgs Change)
        {
            try { this.RefreshDataContext(DataContext); }
            catch (Exception)
            { }
        }

        public void Dispose()
        {
            FactoriesVault.ChangesGetter.RemoveObserver(this);
            this.DisposeChildren();
        }
    }
}
