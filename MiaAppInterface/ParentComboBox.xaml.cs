using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                ItemsSource = GetFilteredParentList(dataItem);
                if (dataItem.ParentId != 0)
                    SelectedItem = dataItem.Factory.GetDataItemsDic()[dataItem.ParentId];
            }
        }

        private List<DataItem> GetFilteredParentList(DataItem dataItem)
        {
            var list = dataItem.Factory.GetDataItemsDic().Select(item => item.Value).Except(new DataItem[]{ dataItem }).ToList();
            return Filter(list, dataItem.Id);
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
    }
}
