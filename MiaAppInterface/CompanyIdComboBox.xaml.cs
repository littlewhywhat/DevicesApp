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
    public partial class CompanyIdComboBox : ComboBox
    {
        public CompanyIdComboBox()
        {
            InitializeComponent();
            Items.Clear();
        }

        private void ComboBox_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext != null)
            {
                var device = DataContext as Device;
                var companyFactory = FactoriesVault.FactoriesDic["Companies"];
                var companyDic = companyFactory.GetDataItemsDic();
                var items = companyDic.Select(item => item.Value).ToList();
                var emptyItem = companyFactory.GetEmptyDataItem();
                emptyItem.Id = 0;
                emptyItem.Name = "Без компании";
                items.Add(emptyItem);
                ItemsSource = items;

                if (companyDic.ContainsKey(device.CompanyId))
                {
                    SelectedItem = companyFactory.GetDataItemsDic()[device.CompanyId];
                }
                else
                {
                    if (device.CompanyId != 0)
                    {
                        device.CompanyId = 0;
                        device.Update();
                    }
                    SelectedItem = emptyItem;
                }
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectedItem != null)
            {
                var device = DataContext as Device;
                var selectedItem = ((Company)this.SelectedItem);
                device.CompanyId = selectedItem.Id;
            }
        }
    }
}
