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
using System.Collections.Specialized;
using InterfaceToDataBase;

namespace InterfaceToClient
{
    /// <summary>
    /// Логика взаимодействия для ParentComboBox.xaml
    /// </summary>
    public partial class OldParentComboBox : ComboBox, Observer
    {
        public OldParentComboBox()
        {
            InitializeComponent();
            Items.Clear();
            
        }

        private void ComboBox_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext != null)
            {
                var dataItem = DataContext as DataItem;
                ItemsSource = GetItems(dataItem);
                if (dataItem.Factory.DataItemsDic.ContainsKey(dataItem.ParentId))
                {
                    SelectedItem = dataItem.Factory.DataItemsDic[dataItem.ParentId];
                }
                else
                    SelectedIndex = 0;
            }
        }
        private List<DataItem> GetItems(DataItem dataItem)
        {
            var items = GetFilteredParentList(dataItem);
            var emptyItem = dataItem.Factory.GetEmptyDataItem();
            emptyItem.Name = "Без родителя";
            items.Insert(0, emptyItem);
            return items;
        }

        private List<DataItem> GetFilteredParentList(DataItem dataItem)
        {
            var DataItemEnumerable = dataItem.Factory.DataItemsDic.Select(item => item.Value).
                Where(item => item.Id != dataItem.Id).ToList();
            if (dataItem.Id == 0)
                return DataItemEnumerable;
            return Filter(DataItemEnumerable, dataItem.Id);
        }
        
        private List<DataItem> Filter(List<DataItem> list, int id)
        {
            int listCount = list.Count;
            for (int i = 0; i < listCount; i++)
            {
                var item = list[i];
                if (item.ParentId == id)
                {
                    Filter(list, item.Id);
                    i = list.IndexOf(item)-1;
                    list.Remove(item);
                    listCount = list.Count;
                }
            }
            return list;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // fires when ItemsSource count changed
            if (IsEnabled)
            {
                var dataItem = (DataItem)DataContext; 
                var selectedItem = ((DataItem)this.SelectedItem);
                if ((dataItem != null) && (selectedItem !=null))
                    dataItem.ParentId = selectedItem.Id;
            }
        }

        public void Update(DataItemControllerChangedEventArgs Change)
        {
            try
            {
                //if ((Change.Action == NotifyCollectionChangedAction.Replace) && (Change.NewDataItem.Id != ((DataItem)DataContext).Id))
                //    return;
                //this.RefreshDataContext(DataContext);
            }
            catch (Exception)
            { }
        }


        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
