using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using DataItemsLibrary;
using System.Windows;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Data;
using DBActionLibrary;

namespace InterfaceToClient
{
    public abstract class ControlManager : Grid
    {
#region
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
        protected abstract FrameworkElement UpdateButton { get; }
        protected abstract FrameworkElement DeleteButton { get; }
        protected abstract FrameworkElement ChangeButton { get; }
        protected abstract FrameworkElement CancelButton { get; }

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
            HideFrameworkElement(position, ChangeButton);
            HideFrameworkElement(!position, UpdateButton);
        }

        protected void HideFrameworkElement(bool position, FrameworkElement element)
        {
            element.Visibility = position ? System.Windows.Visibility.Hidden : System.Windows.Visibility.Visible;
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
