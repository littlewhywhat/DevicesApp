using System.Windows;
using System.Windows.Controls;
using System.Collections.Specialized;
using MiaMain;

namespace MiaAppInterface
{
    /// <summary>
    /// Логика взаимодействия для DataGrid.xaml
    /// </summary>
    public abstract partial class ManagerGrid : Grid
    {

        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        ////public Grid ContentGrid { get { return contentGrid; } set { contentGrid.Children.Add(value); } }

        ////private DataItemsInfoGrid DataItemsGrid
        ////{
        ////    get { return (DataItemsInfoGrid)contentGrid.Children[0]; }
        ////}
        ////private DataItem CurrentDataItem
        ////{
        ////    get { return ((DataItemsChange)DataContext).NewDataItem; }
        ////}

        public abstract DataItemsInfoGrid ContentGrid { get; set; }
        protected abstract IClose Closer { get; }
        protected abstract DataItemsChange CurrentChange { get; }
        protected abstract Button UpdateButton { get; }
        protected abstract Button DeleteButton { get; }
        protected abstract Button ChangeButton { get; }
        protected abstract Button CancelButton { get; }

        private DataItem CurrentDataItem { get { return CurrentChange.NewDataItem; } }

        protected void DeleteClick(RoutedEventArgs e)
        {
            CurrentDataItem.Delete();
            Closer.Close(this);
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

        public void SwitchChangeMode(bool position)
        {
            ContentGrid.ChangeMode = position;
            UpdateButton.IsEnabled = position;
            DeleteButton.IsEnabled = position;
            ChangeButton.IsEnabled = !position;
            CancelButton.IsEnabled = position;
            ContentGrid.IsEnabled = position;
        }

        public void EnableInsertMode()
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
                ((Change.Action == NotifyCollectionChangedAction.Add)&&(Change.NewDataItem.Id == 0)))
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
