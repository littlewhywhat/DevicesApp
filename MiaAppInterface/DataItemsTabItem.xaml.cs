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
        int i =0;
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
                //Console.WriteLine(String.Format("{0} {1}"), currentData.Name, i++);
                if ((e.NewItems != null) && (e.NewItems.Count != 0))
                {
                    var newItem = ((KeyValuePair<int, DataItem>)e.NewItems[0]).Value;
                    if (newItem.Equals(currentData))
                    {
                        newItem.Fill(newItem.Factory.OtherTableFields);
                        DataContext = newItem;
                    }
                    else
                        this.RefreshDataContext(DataContext);
                }
                if ((e.OldItems != null) && (e.OldItems.Count != 0))
                {
                    if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
                    {
                        var deletedItem = ((KeyValuePair<int, DataItem>)e.OldItems[0]).Value;
                        if (currentData.Equals(deletedItem))
                        {
                            CloseTabItem();
                        }
                        else
                            this.RefreshDataContext(DataContext);

                    }

                }

        }


        private void Close_Click(object sender, RoutedEventArgs e)
        {
            CloseTabItem();
        }
        public void CloseTabItem()
        {
            var tabControl = (TabControl)this.Parent;
            tabControl.Items.Remove(this);
            FactoriesVault.FactoriesDic["Companies"].GetDataItemsDic().CollectionChanged -= DeviceTabItem_CollectionChanged;
            FactoriesVault.FactoriesDic["Devices"].GetDataItemsDic().CollectionChanged -= DeviceTabItem_CollectionChanged;
        }

    }
}
