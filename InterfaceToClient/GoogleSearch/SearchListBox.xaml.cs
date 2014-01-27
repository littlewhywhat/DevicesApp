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
    /// Логика взаимодействия для SearchListBox.xaml
    /// </summary>
    public partial class SearchListBox : ListBox
    {
        public SearchListBox()
        {
            InitializeComponent();
        }
        public void Search()
        {
            //Items.Clear();
            var searchCriteria = DataContext.ToString();
            var searchList = searchCriteria.ToLower().Split(new char[] { ' ' }).ToList();
            if (HasItems)
                Items.Filter = item => ((DataItemController)((Control)item).Tag).Search(searchCriteria.ToLower().Split(new char[] {' '}).ToList()) != null;
            else
                Items.Filter = null;
                ItemsSource = FactoriesVault.Dic.Values.Where(factory => factory.TableName != TableNames.DeviceEvents).SelectMany(factory => factory.Search(searchList)).Where(tuple => tuple.Result != null).Select(tuple =>
                    new ListBoxItem() { Content = tuple.Reference.Name, Tag = tuple.Reference, DataContext = tuple.Result });
            //factory.DataItemsDic.ForEach(item =>
            //{
            //    if (item.Key.ToString() == searchCriteria)
            //        Items.Add(new ListBoxItem() { Content = ((Company)item.Value).Name });
            //});
            
            //var block = new TextBlock();
            //block.Text = "hi";
            //var panel = new StackPanel();
            //panel.Children.Add(block);
            //Items.Add(new ListBoxItem() { Content =  panel});
            //Items.Add(new ListBoxItem() { Content = panel });
            //Items.Add(new ListBoxItem() { Content = panel });
            //var grid = Parent as Grid;
            //var popUp = grid.Parent as SearchPopUp;
            //popUp.IsOpen = true;
            //((SearchPopUp)(this.Parent.Parent).IsOpen = true;
        }

    }
}
