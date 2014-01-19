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
    public partial class DataItemsTabItem : TabItem
    {
        public DataItemsTabItem(DataItem dataItem)
        {
            InitializeComponent();
            DataContext = dataItem;
            FactoriesVault.FactoriesDic["Devices"].GetDataItemsDic().CollectionChanged += DeviceTabItem_CollectionChanged;
            FactoriesVault.FactoriesDic["Companies"].GetDataItemsDic().CollectionChanged += DeviceTabItem_CollectionChanged;
        }

        void DeviceTabItem_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            var currentData = DataContext as DataItem;
            if ((e.NewItems != null) && (e.NewItems.Count != 0))
            {
                var newItem = ((KeyValuePair<int, DataItem>)e.NewItems[0]).Value;
                if ((newItem.GetType() == currentData.GetType())&&(newItem.Id == currentData.Id))
                {
                    newItem.Fill(newItem.Factory.OtherTableFields);
                    DataContext = newItem;
                }
                else
                    this.RefreshDataContext(DataContext);
            }
            if ((e.OldItems != null) && (e.OldItems.Count != 0))
                this.RefreshDataContext(DataContext);

        }


        private void Close_Click(object sender, RoutedEventArgs e)
        {
            CloseTabItem();
        }
        public void CloseTabItem()
        {
            var tabControl = (TabControl)this.Parent;
            tabControl.Items.Remove(this);
        }

    }
}
