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
    /// Логика взаимодействия для ParentComboBox.xaml
    /// </summary>
    public partial class ParentComboBox : ComboBox
    {
        public ParentComboBox()
        {
            InitializeComponent();
            Items.Clear();
        }

        private void ComboBox_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext != null)
            {
                var dataItem = DataContext as DataItem;
                var items = GetFilteredParentList(dataItem);
                var emptyItem = dataItem.Factory.GetEmptyDataItem();
                emptyItem.Id = 0;
                emptyItem.Name = "Без родителя";
                items.Add(emptyItem);
                ItemsSource = items;
                var dictionary = dataItem.Factory.GetDataItemsDic();
                if (dataItem.ParentId != 0)
                {
                    SelectedItem = dataItem.Factory.GetDataItemsDic()[dataItem.ParentId];
                }
                else
                    SelectedItem = emptyItem;

            }
        }

        private List<DataItem> GetFilteredParentList(DataItem dataItem)
        {
            var DataItemEnumerable = dataItem.Factory.GetDataItemsDic().Select(item => item.Value);
            if (dataItem.Id == 0)
                return DataItemEnumerable.ToList();
            return Filter(DataItemEnumerable.Where(item => item.Id != dataItem.Id).ToList(), dataItem.Id);
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
            if (SelectedItem != null)
            {
                var dataItem = DataContext as DataItem;
                var selectedItem = ((DataItem)this.SelectedItem);
                dataItem.ParentId = selectedItem.Id;
            }
        }
    }
}
