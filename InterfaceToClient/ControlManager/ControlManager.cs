using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using InterfaceToDataBase;
using System.Windows;
using System.Collections.Specialized;
using System.ComponentModel;

namespace InterfaceToClient
{
    public abstract class ControlManager : Grid
    {
#region
        //public static readonly DependencyProperty DataItemControllerContextProperty = DependencyProperty.Register("DataItemControllerContext", typeof(DataItemController),
        //                        typeof(ControlManager),
        //                        new FrameworkPropertyMetadata(null,
        //                                FrameworkPropertyMetadataOptions.Inherits,
        //                                new PropertyChangedCallback(OnDataItemControllerContextChanged)));

        //internal static readonly EventPrivateKey DataItemControllerContextChangedKey = new EventPrivateKey();


        //public event DependencyPropertyChangedEventHandler DataItemControllerContextChanged
        //{
        //    add { EventHandlersStoreAdd(DataItemControllerContextChangedKey, value); }
        //    remove { EventHandlersStoreRemove(DataItemControllerContextChangedKey, value); }
        //}

        ///// 

        /////     DataContext Property
        ///// 

        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        //[Localizability(LocalizationCategory.NeverLocalize)]
        //public object DataItemControllerContext
        //{
        //    get { return GetValue(DataItemControllerContextProperty); }
        //    set { SetValue(DataItemControllerContextProperty, value); }
        //}

        //private static void OnDataItemControllerContextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    ((ControlManager)d).RaiseDependencyPropertyChanged(DataContextChangedKey, e);
        //}
        //private void RaiseDependencyPropertyChanged(EventPrivateKey key, DependencyPropertyChangedEventArgs args)
        //{
        //    var store = EventHandlersStore;
        //    if (store != null)
        //    {
        //        Delegate handler = store.Get(key);
        //        if (handler != null)
        //        {
        //            ((DependencyPropertyChangedEventHandler)handler)(this, args);
        //        }
        //    }
        //}
#endregion

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public DataItemsInfoGrid ContentGrid 
        { 
            get { return (DataItemsInfoGrid)SocketGrid.Children[0]; } 
            set { SocketGrid.Children.Add(value); } 
        }
        public abstract IClose Closer { get; set; }
        protected abstract Grid SocketGrid { get; }
        public abstract DataItemController CurrentDataItemController { get; }
        protected abstract Button UpdateButton { get; }
        protected abstract Button DeleteButton { get; }
        protected abstract Button ChangeButton { get; }
        protected abstract Button CancelButton { get; }

        //public DataItemController CurrentDataItemController { get { return CurrentChange.NewController; } }

        private void Close()
        {
            var closer = Closer;
            if (closer != null)
                closer.Close(this);
        }

        protected void DeleteClick(RoutedEventArgs e)
        {
            var dataItemController = CurrentDataItemController;
            Close();
            dataItemController.Delete();
            e.Handled = true;
        }

        protected void UpdateClick()
        {
            SwitchChangeMode(false);
            CurrentDataItemController.ChangeInDb();
        }

        protected void ChangeClick()
        {
            CurrentDataItemController.ChangeMode = true;
            SwitchChangeMode(true);
            //ContentGrid.Refresh(dataItemControllerNew);
        }

        protected void CancelClick()
        {
            SwitchChangeMode(false);
            CurrentDataItemController.ChangeMode = false;
        }

        public virtual void SwitchChangeMode(bool position)
        {
            UpdateButton.IsEnabled = position;
            DeleteButton.IsEnabled = position;
            ChangeButton.IsEnabled = !position;
            CancelButton.IsEnabled = position;
            SocketGrid.IsEnabled = position;
        }

        public virtual void EnableInsertMode()
        {
            SwitchChangeMode(true);
            CancelButton.IsEnabled = false;
            DeleteButton.IsEnabled = false;
        }

        protected void PossessChangeOfCurrentChange()
        {
            //if ((CurrentChange.Action == NotifyCollectionChangedAction.Remove) ||
            //((CurrentChange.Action == NotifyCollectionChangedAction.Add) && (CurrentChange.NewController.InsertMode)))
            //{

            //        ContentGrid.Refresh(CurrentChange.NewController);
            //        EnableInsertMode(); 
            //}
            //if (!ContentGrid.ChangeMode)
            //    ContentGrid.Refresh(CurrentChange.NewController);
            //else
            //    ContentGrid.RefreshComboBoxes();
        }

        
    }
}
