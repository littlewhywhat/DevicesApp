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
        public DataItemController CurrentDataItemController { get { return DataContextControl.Element; } }
        public DataContextControl<DataItemController> DataContextControl { get { return (DataContextControl<DataItemController>)DataContext; } }
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
                if  (Change.NewController == CurrentDataItemController ||
                    Change.OldController == CurrentDataItemController)
                {
                    if ((Change.Action == NotifyCollectionChangedAction.Remove))
                        CurrentDataItemController.GetInInsertMode();
                    else 
                        DataContextControl.SetElement(Change.NewController);
                    return;
                }
                DataContextControl.Refresh();
                
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
