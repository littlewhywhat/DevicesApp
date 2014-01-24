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
    /// Логика взаимодействия для CompanyIdComboBox.xaml
    /// </summary>
    public partial class CompanyIdComboBox : ComboBox, Observer, IDisposable
    {
        CompaniesFactory Factory;
        public CompanyIdComboBox()
        {
            InitializeComponent();
            Items.Clear();
            Factory = FactoriesVault.FactoriesDic[TableNames.Companies] as CompaniesFactory;
            FactoriesVault.ChangesGetter.AddObserver(this, new string[] { TableNames.Companies });
        }

        private void ComboBox_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext != null)
            {
                var device = DataContext as Device;
                ItemsSource = GetItems(device);
                if (Factory.GetDataItemsDic().ContainsKey(device.CompanyId))
                {
                    SelectedItem = Factory.GetDataItemsDic()[device.CompanyId];
                }
                else
                    SelectedIndex = 0;
            }
        }
        private List<DataItem> GetItems(DataItem dataItem)
        {
            var items = Factory.GetDataItemsDic().Select(item => item.Value).ToList();
            var emptyItem = Factory.GetEmptyDataItem();
            emptyItem.Name = "Без компании";
            items.Insert(0, emptyItem);
            return items;
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // fires when ItemsSource count changed
            if (IsEnabled)
            {
                var device = (Device)DataContext;
                var selectedItem = ((Company)this.SelectedItem);
                if ((device != null) && (selectedItem != null))
                    device.CompanyId = selectedItem.Id;
            }
        }

        public void Update(DataItemsChange Change)
        {
            try
            {
                this.RefreshDataContext(DataContext);
            }
            catch(Exception)
            {

            }

        }


        public void Dispose()
        {
            FactoriesVault.ChangesGetter.RemoveObserver(this);
            this.DisposeChildren();
        }
    }
}
