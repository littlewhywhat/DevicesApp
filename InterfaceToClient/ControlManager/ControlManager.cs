using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using InterfaceToDataBase;
using System.Windows;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Data;

namespace InterfaceToClient
{
    public abstract class ControlManager : Grid
    {
#region
        //public static readonly DependencyProperty DataItemControllerContextProperty = 
        //    DependencyProperty.Register("InsertMode", typeof(bool),
        //                        typeof(ControlManager),
        //                        new FrameworkPropertyMetadata(null,
        //                                FrameworkPropertyMetadataOptions.Inherits,
        //                                new PropertyChangedCallback(OnInsertModeChanged)));

        //private static void OnInsertModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    ((ControlManager)d).RaiseDependencyPropertyChanged(InsertModeChangedKey, e);
        //}

        //internal static readonly EventPrivateKey InsertModeChangedKey = new EventPrivateKey();


        ////public event DependencyPropertyChangedEventHandler DataItemControllerContextChanged
        ////{
        ////    add { EventHandlersStoreAdd(DataItemControllerContextChangedKey, value); }
        ////    remove { EventHandlersStoreRemove(DataItemControllerContextChangedKey, value); }
        ////}

        /////// 

        ///////     DataContext Property
        /////// 

        ////[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        ////[Localizability(LocalizationCategory.NeverLocalize)]
        ////public object DataItemControllerContext
        ////{
        ////    get { return GetValue(DataItemControllerContextProperty); }
        ////    set { SetValue(DataItemControllerContextProperty, value); }
        ////}

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
        public static readonly DependencyProperty InsertModeProperty =
            DependencyProperty.Register("InsertMode", typeof(bool), typeof(ControlManager), 
            new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsRender, 
                new PropertyChangedCallback(InsertMode_PropertyChanged)));

        private static void InsertMode_PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.Dispatcher.BeginInvoke((Action)(() => 
                {
                    if (((ControlManager)d).InsertMode)
                        ((ControlManager)d).EnableInsertMode();
                }));
        }

        public bool InsertMode
        {
            get
            {
                return (bool)this.GetValue(InsertModeProperty);
            }
            set
            {
                this.SetValue(InsertModeProperty, value);
            }
        }

        
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

        private void PerformDBAction(Action action)
        {
            try { action.Invoke(); }
            catch (DBActionException exception) { exception.ShowMessageBox(); }
        }
        private void Delete()
        {
            CurrentDataItemController.Delete();
            Close();
        }
        protected void DeleteClick(RoutedEventArgs e)
        {
            PerformDBAction(Delete);
            e.Handled = true;
        }

        protected void UpdateClick()
        {
            PerformDBAction(Update);
        }
        private void Update()
        {
            CurrentDataItemController.ChangeInDb();
            SwitchChangeMode(false);
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
        protected virtual void EnableInsertMode()
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
