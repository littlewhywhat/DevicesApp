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
using DataItemsLibrary;
namespace InterfaceToClient
{
    /// <summary>
    /// Логика взаимодействия для SearchListBox.xaml
    /// </summary>
    public partial class SearchListBox : ListView
    {
        public SearchListBox()
        {
            InitializeComponent();
        }
        public DataItemControllersDictionary SearchDictionary { get; set; } 

        public void Search()
        {
            //Items.Clear();
            var searchCriteria = DataContext.ToString();
            var searchList = searchCriteria.ToLower().Split(new char[] { ' ' }).ToList();
            if (searchList.Count != 0)
            {
                if (HasItems)
                {
                    SetFilter(new List<string>() { searchList.Last() });
                }
                else
                {
                    Items.Filter = null;
                    var result = SearchDictionary.Search(searchList);
                    ItemsSource = result;
                    
                }
            }
   
        }
        private void SetFilter(List<string> searchList)
        {
            Items.Filter = item => SearchInList((FrameworkElement)item, searchList);
        }

        private bool SearchInList(FrameworkElement item, List<string> searchList)
        {
            var result = ((DataItemController)item.DataContext).Search(searchList);
            if (result != null)
            {
                item.Tag = result;
                return true;
            }
            return false;
                
        }
    }
}
