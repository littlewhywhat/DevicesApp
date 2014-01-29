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
    /// Логика взаимодействия для DataItemComboBox.xaml
    /// </summary>
    public abstract partial class DataItemsComboBox : ComboBox
    {
        public DataItemsComboBox()
        {
            InitializeComponent();
            Items.Clear();
        }

        private void ComboBox_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext != null)
            {
                var CurrentController = GetCurrentSelection();
                ItemsSource = GetControllers();
                if (Items.SourceCollection.Cast<DataItemController>().Contains(CurrentController))
                    SelectedItem = CurrentController;
                else
                    SelectedIndex = 0;
            }
            else
                ItemsSource = null;
        }

        protected abstract DataItemController GetCurrentSelection();
        protected abstract IEnumerable<DataItemController> GetControllersWithoutEmpty();
        protected abstract DataItemController GetEmptyController();
        protected abstract void ChangeCurrentDataContextBySelectedItem();

        private List<DataItemController> GetControllers()
        {
            var result = GetControllersWithoutEmpty().ToList();
            result.Insert(0, GetEmptyController());
            return result;
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ChangeMode)
            {
                if ((!IsCurrentDataContextNull) && (!IsCurrentSelectedItemNull))
                    ChangeCurrentDataContextBySelectedItem();
            }
        }

        private bool ChangeMode { get { return IsEnabled; } }
        protected DataItemController CurrentDataContext { get { return DataContext as DataItemController; } }
        protected DataItemController CurrentSelectedItem { get { return SelectedItem as DataItemController; } }
        protected bool IsCurrentDataContextNull { get { return CurrentDataContext == null; } }
        protected bool IsCurrentSelectedItemNull { get { return CurrentSelectedItem == null; } }
    }
}
