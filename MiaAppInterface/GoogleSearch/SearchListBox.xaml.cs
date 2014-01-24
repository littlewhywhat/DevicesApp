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
    /// Логика взаимодействия для SearchListBox.xaml
    /// </summary>
    public partial class SearchListBox : ListBox
    {
        public SearchListBox()
        {
            InitializeComponent();
        }
        public void Search(DataItemsFactory factory1)
        {
            //Items.Clear();
            var searchCriteria = DataContext.ToString();
            if (HasItems)
                Items.Filter = item => ((DataItem)((Control)item).Tag).Search(searchCriteria.ToLower().Split(new char[] {' '}).ToList()) != null;
            else
                Items.Filter = null;
                ItemsSource = FactoriesVault.FactoriesDic.Where(factory => factory.Value.TableName != TableNames.DeviceEvents).SelectMany(factory => factory.Value.GetDataItemsDic().Select(item => new Result
                (item.Value.Search(searchCriteria.ToLower().Split(new char[] {' '}).ToList()), item.Value)).Where(tuple => tuple.result != null).Select(tuple =>
                    new ListBoxItem() { Content = tuple.reference.Name, Tag = tuple.reference, DataContext = tuple.result }));
            //factory.GetDataItemsDic().ForEach(item =>
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
        class Result
        {
            public string result;
            public DataItem reference;
            public Result(string Result, DataItem correspondingDataItem)
            {
                result = Result;
                reference = correspondingDataItem;
            }
        }
    }
}
