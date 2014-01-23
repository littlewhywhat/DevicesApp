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
using MiaMain;
namespace MiaAppInterface
{
    public partial class DataItemsTabItem : TabItem, MiaMain.Observer, IClose
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

        public void Update(DataItemsChange Change)
        {
            if (((Change.NewDataItem != null) &&(Change.NewDataItem.Id == ((DataItemsChange)DataContext).NewDataItem.Id)) || 
                ((Change.OldDataItem != null) &&(Change.OldDataItem.Id == ((DataItemsChange)DataContext).NewDataItem.Id)))
            {
                if ((Change.Action == NotifyCollectionChangedAction.Replace)) 
                    Replace(Change);
                if ((Change.Action == NotifyCollectionChangedAction.Remove))
                    Remove(Change);
                DataContext = Change;
                return;
            }
            this.RefreshDataContext(DataContext);
        }

        public void Remove(DataItemsChange Change)
        {
            Change.OldDataItem.Id = 0;
            Change.NewDataItem = Change.OldDataItem;
        }

        public void Replace(DataItemsChange Change)
        {
            Change.NewDataItem.Fill(Change.NewDataItem.Factory.OtherTableFields);
        }

        void IClose.Close(ManagerGrid manager)
        {
            CloseTabItem();
        }
    }
}
