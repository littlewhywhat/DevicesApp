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
    public partial class DataItemsTabItem : TabItem, Observer, IClose
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
            Dispose();        
        }

        public void Update(DataItemControllerChangedEventArgs Change)
        {
            try
            {
                if (((Change.NewController != null) && (Change.NewController.Id == ((DataItemControllerChangedEventArgs)DataContext).NewController.Id)) ||
                    ((Change.OldController != null) && (Change.OldController.Id == ((DataItemControllerChangedEventArgs)DataContext).NewController.Id)))
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

        public void Remove(DataItemControllerChangedEventArgs Change)
        {
            var tabItem = (TabItemGrid)this.FindFirstChild<TabItemGrid>();
            if (tabItem != null)
            {
                tabItem.Update.Focus();
                var tabItemDataItem = (DataItemController)((FrameworkElement)tabItem.ContentGrid).DataContext;
                tabItemDataItem.GetInInsertMode();
                DataContext = new DataItemControllerChangedEventArgs() { 
                    NewController = tabItemDataItem, 
                    Action = NotifyCollectionChangedAction.Add };

            }
        }

        void IClose.Close(ControlManager manager)
        {
            CloseTabItem();
        }


        public void Dispose()
        {
            FactoriesVault.ChangesGetter.RemoveObserver(this);
            ((FrameworkElement)Content).DisposeChildren();   
        }
    }
}
