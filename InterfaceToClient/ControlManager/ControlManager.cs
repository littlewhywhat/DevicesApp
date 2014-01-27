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
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public DataItemsInfoGrid ContentGrid 
        { 
            get { return (DataItemsInfoGrid)SocketGrid.Children[0]; } 
            set { SocketGrid.Children.Add(value); } 
        }
        public abstract IClose Closer { get; set; }
        protected abstract Grid SocketGrid { get; }
        protected abstract DataItemControllerChangedEventArgs CurrentChange { get; }
        protected abstract Button UpdateButton { get; }
        protected abstract Button DeleteButton { get; }
        protected abstract Button ChangeButton { get; }
        protected abstract Button CancelButton { get; }

        public DataItemController CurrentDataItemController { get { return CurrentChange.NewController; } }

        private void Close()
        {
            var closer = Closer;
            if (closer != null)
                closer.Close(this);
        }

        protected void DeleteClick(RoutedEventArgs e)
        {
            CurrentDataItemController.Delete();
            Close();
            e.Handled = true;
        }

        protected void UpdateClick()
        {
            SwitchChangeMode(false);
            ContentGrid.CurrentDataItem.ChangeInDb();
        }

        protected void ChangeClick()
        {
            var dataItemControllerNew = CurrentDataItemController.Clone();
            SwitchChangeMode(true);
            ContentGrid.Refresh(dataItemControllerNew);
        }

        protected void CancelClick()
        {
            SwitchChangeMode(false);
            ContentGrid.Refresh(CurrentDataItemController);
        }

        public virtual void SwitchChangeMode(bool position)
        {
            ContentGrid.ChangeMode = position;
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
            if ((CurrentChange.Action == NotifyCollectionChangedAction.Remove) ||
            ((CurrentChange.Action == NotifyCollectionChangedAction.Add) && (CurrentChange.NewController.InsertMode)))
            {

                    ContentGrid.Refresh(CurrentChange.NewController);
                    EnableInsertMode(); 
            }
            if (!ContentGrid.ChangeMode)
                ContentGrid.Refresh(CurrentChange.NewController);
            else
                ContentGrid.RefreshComboBoxes();
        }

        
    }
}
