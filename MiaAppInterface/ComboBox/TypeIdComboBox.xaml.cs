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
    /// Логика взаимодействия для TypeIdComboBox.xaml
    /// </summary>
    public partial class TypeIdComboBox : ComboBox, Observer, IDisposable
    {
        DeviceTypesFactory Factory;
        public TypeIdComboBox()
        {
            InitializeComponent();
            Items.Clear();
            Factory = FactoriesVault.FactoriesDic[TableNames.DeviceTypes] as DeviceTypesFactory;
            FactoriesVault.ChangesGetter.AddObserver(this, new string[] { TableNames.DeviceTypes });
        }

        private void ComboBox_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext != null)
            {
                var device = DataContext as Device;
                ItemsSource = GetItems(device);
                if (Factory.GetDataItemsDic().ContainsKey(device.TypeId))
                {
                    SelectedItem = Factory.GetDataItemsDic()[device.TypeId];
                }
                else
                    SelectedIndex = 0;
            }
        }
        private List<DataItem> GetItems(DataItem dataItem)
        {
            var items = Factory.GetDataItemsDic().Values.Where(deviceType => !((DeviceType)deviceType).IsMarker).ToList();
            var emptyItem = Factory.GetEmptyDataItem();
            emptyItem.Name = "Тип неопределен";
            items.Insert(0, emptyItem);
            return items;
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

        public void Update(DataItemsChange Change)
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
