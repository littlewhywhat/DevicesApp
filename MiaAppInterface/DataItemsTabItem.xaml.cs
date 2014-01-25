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
            ((FrameworkElement)Content).DisposeChildren();           
        }

        public void Update(DataItemsChange Change)
        {
            try
            {
                if (((Change.NewDataItem != null) && (Change.NewDataItem.Id == ((DataItemsChange)DataContext).NewDataItem.Id)) ||
                    ((Change.OldDataItem != null) && (Change.OldDataItem.Id == ((DataItemsChange)DataContext).NewDataItem.Id)))
                {
                    if ((Change.Action == NotifyCollectionChangedAction.Remove))
                        Remove(Change);
                    else 
                        DataContext = Change;
                    return;
                }
                this.RefreshDataContext(DataContext);
                
            }
            catch (Exception)
            { }
        }

        public void Remove(DataItemsChange Change)
        {
            var tabItem = (TabItemGrid)this.FindFirstChild<TabItemGrid>();
            if (tabItem != null)
            {
                tabItem.Update.Focus();
                var tabItemDataItem = (DataItem)((FrameworkElement)tabItem.ContentGrid).DataContext;
                tabItemDataItem.Id = 0;
                DataContext = new DataItemsChange() { 
                    NewDataItem = tabItemDataItem, 
                    Action = NotifyCollectionChangedAction.Add };

            }
        }

        void IClose.Close(ControlManager manager)
        {
            CloseTabItem();
        }
    }
}
