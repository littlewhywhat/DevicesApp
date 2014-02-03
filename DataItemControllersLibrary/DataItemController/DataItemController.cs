using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataItemsLibrary;
using System.ComponentModel;
using System.Windows;
using DBActionLibrary;

namespace DataItemControllersLibrary
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
        protected abstract bool IsTheSameByType(DataItemController controller);

        public DataItemController(DataItem dataItem, DataItemControllersFactory factory)
        {
            this.dataItem = dataItem;
            Factory = factory;
        }
        private bool changeMode = false;

        const string _Name = "Name";
        const string _NotInsertMode = "NotInsertMode";
        const string _InsertMode = "InsertMode";
        protected virtual void OnPropertyChanged()
        {
            OnPropertyChanged(_Name);
            OnPropertyChanged(_NotInsertMode);
            OnPropertyChanged(_InsertMode);
        }
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        protected DataItem dataItem;
        protected DataItem clone;
        protected DataItem DataItem { get { return ChangeMode ? clone : dataItem; } }
        protected DataItem GetNewClone() { return dataItem.Clone(); }

        internal virtual Dictionary<string, string> GetSearchPropertyValueDic()
        {
            var dictionary = new Dictionary<string, string>();
            GetType().GetProperties().ToList().ForEach(dataItemProperty =>
            {
                if ((DataItem.Factory.SearchTableFields.Contains(dataItemProperty.Name)))
                    dictionary.Add(dataItemProperty.Name, dataItemProperty.GetValue(this, null).ToString());
            });
            return dictionary;
        }
        public DataItemControllersFactory Factory;
        public virtual void Refresh()
        {
            OnPropertyChanged();
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void RefreshDataItemByController(DataItemController controller)
        {
            dataItem = controller.dataItem;
            OnPropertyChanged();
        }
        public int Id { get { return DataItem.Id; } set { DataItem.Id = value; } }
        public string Name
        {
            get { return DataItem.Name; }
            set { DataItem.Name = value; }
        }
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
        public virtual void ChangeInDb()
        {
            DataItem.ChangeInDb();
            ChangeMode = false;
        }
        public virtual void Delete()
        {
            DataItem.Delete();
        }

        private bool SearchContains(string value, List<string> searchList)
        {
            bool result = false;
            for (int i = 0; i < searchList.Count; i++)
            {
                var listItem = searchList[i];
                if (value.Contains(listItem))
                {
                    result = true;
                    searchList.Remove(listItem);
                    i--;
                }
            }
            return result;
        }
        public String Search(List<string> searchListParameter)
        {
            if (searchListParameter.Count != 0)
            {
                var searchList = new List<string>(searchListParameter);
                var resultCollection = GetSearchPropertyValueDic().Values.Where(value => SearchContains(value.ToLower(), searchList)).ToArray();
                if (searchList.ToList().Count == 0)
                    return String.Join(" ", resultCollection);
            }
            return null;
        }
    }
}
