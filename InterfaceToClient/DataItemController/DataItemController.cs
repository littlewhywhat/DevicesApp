using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using InterfaceToDataBase;
using System.ComponentModel;
using System.Windows;

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
        public DataItemController(DataItem dataItem, DataItemControllersFactory factory)
        {
            this.dataItem = dataItem;
            Factory = factory;
        }
        public DataItemControllersFactory Factory;
        protected abstract DataItemControllersDictionary GetDictionary();
        
        public FrameworkElement GetPanel()
        {
            var border = Factory.GetPanel();
            border.DataContext = this;
            return border;
        }

        #region propertychanged implementation
        protected const string _Name = "Name";
        protected const string _ParentOrDefault = "ParentOrDefault";
        protected const string _Parents = "Parents";
        protected const string _NotInsertMode = "NotInsertMode";
        protected const string _InsertMode = "InsertMode";
        protected virtual void OnPropertyChanged()
        {
            OnPropertyChanged(_Name);
            OnPropertyChanged(_ParentOrDefault);
            OnPropertyChanged(_Parents);
            OnPropertyChanged(_NotInsertMode);
            OnPropertyChanged(_InsertMode);
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void Refresh()
        {
            if (ChangeMode)
                CheckIds();
            OnPropertyChanged();
        }
        public virtual void CheckIds()
        {
            if (!ParentExist)
                DataItem.ParentId = 0;
        }
        public bool ParentExist 
        { 
            get 
            {
                try { return GetPossibleParents().ToDictionary(dataItemController => dataItemController.Id).ContainsKey(DataItem.ParentId); }
                catch { return false; }
            } 
        }
        public void RefreshAndCheckIdsAfterDelete(DataItemController OldDataItemController)
        {
            if (ChangeMode)
            {
                if (OldDataItemController.Factory == Factory)
                    if (DataItem.ParentId == OldDataItemController.Id)
                        Parent = GetDictionary().GetWithoutParentController();
            }
            
        }
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        #region Properties
        public int Id { get { return DataItem.Id; } set { DataItem.Id = value; } }
        public string Name
        {
            get { return DataItem.Name; }
            set { DataItem.Name = value; }
        }
        public bool HasParents { get { return DataItem.ParentId != 0; } }
        
        public virtual DataItemController Parent
        {
            get { return HasParents ? GetDictionary().DataItemControllersDic[DataItem.ParentId] : null; }
            set { DataItem.ParentId = value.Id; }
        }
        public DataItemController ParentOrDefault
        {
            get { return HasParents ? Parent : GetDictionary().GetWithoutParentController(); }
            set { Parent = value; }
        }

        public List<DataItemController> Parents
        {
            get 
            { 
                var result = GetPossibleParents().ToList();
                result.Insert(0, GetDictionary().GetWithoutParentController());
                if (!result.Contains(ParentOrDefault))
                    result.Add(ParentOrDefault);
                return result;
            }
        }

        #endregion
        bool changeMode = false;
        private DataItem dataItem;
        protected DataItem clone;
        protected DataItem DataItem { get { return ChangeMode ? clone : dataItem; } }
        public bool ChangeMode
        {
            get { return changeMode; }
            set
            {
                clone = value ? dataItem.Clone() : null;
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
        
        public bool HasTheSameParentId(int id) { return DataItem.ParentId == id; }

        public bool IsChildOf(int id) { return DataItem.ParentId == id; }
        public bool IsChildOf(DataItemController controller) { return IsChildOf(controller.Id); }

        private IEnumerable<DataItemController> GetPossibleParents()
        {
            return GetDictionary().GetPossibleParents(this);
        }

        private List<DataItemController> GetChildren()
        {
            return GetDictionary().GetChildrenByParentId(Id).ToList();
        }

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

        internal void ChangeInDb()
        {
            DataItem.ChangeInDb();
            ChangeMode = false;
        }

        public void Delete()
        {
            DeleteReferences();
            DataItem.Delete();
        }

        protected void DeleteReferences()
        {
            DBHelper.PerformDBAction(Connection.GetConnection(), new PerformTransactionOnList(GetDeleteReferencesActions()));
        }

        protected virtual List<TransactionData> GetDeleteReferencesActions()
        {
            return GetChildren().Select(child => (TransactionData)child.DeleteParentTransaction()).ToList();
        }

        public UpdateDataItem DeleteParentTransaction()
        {
            var clone = DataItem.Clone();
            clone.ParentId = 0;
            return new UpdateDataItem(clone);
        }


        private bool Equals(DataItemController controller)
        {
            return (Id == controller.Id) && (IsTheSameByType(controller));
        }

        public abstract bool IsTheSameByType(DataItemController controller);

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

        public void RefreshDataItemByController(DataItemController controller)
        {
            dataItem = controller.dataItem;
            OnPropertyChanged();
        }
        
    }
}
