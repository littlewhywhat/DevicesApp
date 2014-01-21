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
    public partial class DataItemsTabItem : TabItem, MiaMain.Observer
    {
        public DataItemsTabItem()
        {
            InitializeComponent();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            CloseTabItem();
        }

        public void CloseTabItem()
        {
            var tabControl = (TabControl)this.Parent;
            tabControl.Items.Remove(this);
            FactoriesVault.ChangesGetter.RemoveObserver(this);
        }


        public void Add(DataItem dataItemNew)
        {
            this.RefreshDataContext(DataContext);
        }

        public void Remove(DataItem dataItemOld)
        {
            if (dataItemOld.Id == ((DataItem)DataContext).Id)
            {
                ((ManagerGrid)Content).SwitchChangeMode(false);
                ((DataItem)DataContext).Id = 0;
                this.RefreshDataContext(DataContext);
                ((ManagerGrid)Content).EnableInsertMode();
            }
            this.RefreshDataContext(DataContext);

        }

        public void Replace(DataItem dataItemNew, DataItem dataItemOld)
        {
            if (dataItemNew.Id == ((DataItem)DataContext).Id)
            {
                dataItemNew.Fill(dataItemNew.Factory.OtherTableFields);
                DataContext = dataItemNew;
            }
            else
                this.RefreshDataContext(DataContext);
        }
    }
}
