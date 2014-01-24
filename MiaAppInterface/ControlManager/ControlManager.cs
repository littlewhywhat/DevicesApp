using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using MiaMain;
using System.Windows;
using System.Collections.Specialized;
using System.ComponentModel;

namespace MiaAppInterface
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
        protected abstract DataItemsChange CurrentChange { get; }
        protected abstract Button UpdateButton { get; }
        protected abstract Button DeleteButton { get; }
        protected abstract Button ChangeButton { get; }
        protected abstract Button CancelButton { get; }

        public DataItem CurrentDataItem { get { return CurrentChange.NewDataItem; } }

        private void Close()
        {
            var closer = Closer;
            if (closer != null)
                closer.Close(this);
        }

        protected void DeleteClick(RoutedEventArgs e)
        {
            CurrentDataItem.Delete();
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
            var dataItemNew = CurrentDataItem.Clone();
            SwitchChangeMode(true);
            ContentGrid.Refresh(dataItemNew);
        }

        protected void CancelClick()
        {
            SwitchChangeMode(false);
            ContentGrid.Refresh(CurrentDataItem);
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
            ((CurrentChange.Action == NotifyCollectionChangedAction.Add) && (CurrentChange.NewDataItem.Id == 0)))
            {
                ContentGrid.Refresh(CurrentChange.NewDataItem);
                EnableInsertMode();
            }
            if (!ContentGrid.ChangeMode)
                ContentGrid.Refresh(CurrentChange.NewDataItem);
            else
                ContentGrid.RefreshComboBoxes();
        }

        private void Grid_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext != null)
            {
                var Change = (DataItemsChange)DataContext;
                if ((Change.Action == NotifyCollectionChangedAction.Remove) ||
                ((Change.Action == NotifyCollectionChangedAction.Add) && (Change.NewDataItem.Id == 0)))
                {
                    ContentGrid.Refresh(Change.NewDataItem);
                    EnableInsertMode();
                }
                if (!ContentGrid.ChangeMode)
                    ContentGrid.Refresh(Change.NewDataItem);
                else
                    ContentGrid.RefreshComboBoxes();
            }
        }
    }
}
