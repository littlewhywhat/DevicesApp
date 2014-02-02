using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using DataItemsLibrary;
using System.ComponentModel;
using System.Windows;
using DBActionLibrary;

namespace InterfaceToClient
{
    public abstract class DataItemController : INotifyPropertyChanged
    {
        public static bool operator == (DataItemController controller1, DataItemController controller2)
        {
            if (Equals(controller1, controller2))
                return true;
            if (Equals(controller1, null) || Equals(controller2, null))
                return false;
            return controller1.Equals(controller2);
        }
        public static bool operator != (DataItemController controller1, DataItemController controller2)
        {
            return !(controller1 == controller2);
        }
        private bool Equals(DataItemController controller)
        {
            return (Id == controller.Id) && (IsTheSameByType(controller));
        }
        public abstract bool IsTheSameByType(DataItemController controller);

        public DataItemController(DataItem dataItem, DataItemControllersFactory factory)
        {
            this.dataItem = dataItem;
            Factory = factory;
        }
        public DataItemControllersFactory Factory;
        protected abstract DataItemControllersDictionary GetDictionary();

        #region propertychanged implementation
        protected const string _Name = "Name";
        protected const string _NotInsertMode = "NotInsertMode";
        protected const string _InsertMode = "InsertMode";
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        protected virtual void OnPropertyChanged()
        {
            OnPropertyChanged(_Name);
            OnPropertyChanged(_NotInsertMode);
            OnPropertyChanged(_InsertMode);
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void RefreshDataItemByController(DataItemController controller)
        {
            dataItem = controller.dataItem;
            OnPropertyChanged();
        }

        public virtual void Refresh()
        {
            OnPropertyChanged();
        }
        
        #endregion
        #region Properties
        public int Id { get { return DataItem.Id; } set { DataItem.Id = value; } }
        public string Name
        {
            get { return DataItem.Name; }
            set { DataItem.Name = value; }
        }
        
        #endregion
        bool changeMode = false;
        protected DataItem dataItem;
        protected DataItem clone;
        protected DataItem DataItem { get { return ChangeMode ? clone : dataItem; } }
        protected DataItem GetNewClone() { return dataItem.Clone(); }
        public bool ChangeMode
        {
            get { return changeMode; }
            set
            {
                clone = value ? GetNewClone() : null;
                changeMode = value;
                OnPropertyChanged();
            }
        }
        public void GetInInsertMode()
        {
            if (ChangeMode)
                dataItem = clone;
            ChangeMode = false;
            DataItem.Id = 0;
            OnPropertyChanged(_NotInsertMode);
            OnPropertyChanged(_InsertMode);
        }
        public bool InsertMode { get { return DataItem.Id == 0; } set { } }
        public bool NotInsertMode { get { return !InsertMode; } }

        public virtual Dictionary<string, string> GetSearchPropertyValueDic()
        {
            var dictionary = new Dictionary<string, string>();
            GetType().GetProperties().ToList().ForEach(dataItemProperty =>
            {
                if ((DataItem.Factory.SearchTableFields.Contains(dataItemProperty.Name)))
                    dictionary.Add(dataItemProperty.Name, dataItemProperty.GetValue(this, null).ToString());
            });
            return dictionary;
        }

        internal virtual void ChangeInDb()
        {
            DataItem.ChangeInDb();
            ChangeMode = false;
        }

        public virtual void Delete()
        {
            DataItem.Delete();
        }

        public DataItemsTabItem GetTabItem()
        {
            DataItem.Fill(DataItem.Factory.OtherTableFields);
            return Factory.GetTabItem(this);
        }

        public ListBoxItemGrid GetListBoxItemGrid()
        {
            DataItem.Fill(DataItem.Factory.OtherTableFields);
            return Factory.GetListBoxItemGrid(this);
        }

        public FrameworkElement GetPanel()
        {
            var border = Factory.GetPanel();
            border.DataContext = this;
            return border;
        }
        public FrameworkElement Panel { get { return GetPanel(); } }
        
    }
}
