using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using InterfaceToDataBase;

namespace InterfaceToClient
{
    public abstract class DataItemController
    {
        protected DataItem DataItem;
        public DataItemControllersFactory Factory;
        public DataItemController(DataItem dataItem, DataItemControllersFactory factory)
        {
            DataItem = dataItem;
            Factory = factory;
        }

        public int Id { get { return DataItem.Id; } set { DataItem.Id = value; } }
        public string Name { get { return DataItem.Name; } set { DataItem.Name = value; } }


        public bool HasParents { get { return DataItem.ParentId != 0; } }
        protected DataItem DataItemParent { get { return DataItem.Factory.DataItemsDic[DataItem.ParentId]; }  }
        public bool InsertMode { get { return DataItem.Id == 0; } }
        public void GetInInsertMode() { DataItem.Id = 0; }
        public virtual DataItemController Parent 
        {
            get { return HasParents ? Factory.GetDataItemController(DataItem.ParentId) : Factory.GetEmptyController(); }
            set { DataItem.ParentId = value.DataItem.Id; }
        }

        public IEnumerable<DataItemController> GetPossibleParents()
        {
            return Factory.GetPossibleParents(this);
        }

        private List<DataItem> GetChildren()
        {
            return Factory.DataItemsDic.GetChildrenByParentId(Id).ToList();
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
            return GetChildren().Select(child =>
                {
                    new FillDataItem(child, child.Factory.OtherTableFields).Act(Connection.GetConnection());
                    child.ParentId = 0;
                    return (TransactionData)new UpdateDataItem(child);
                }).ToList();
        }

        public virtual Dictionary<string, string> GetSearchPropertyValueDic()
        {
            var dictionary = new Dictionary<string, string>();
            GetType().GetProperties().ForEach(dataItemProperty =>
            {
                if ((DataItem.Factory.SearchTableFields.Contains(dataItemProperty.Name)))
                    dictionary.Add(dataItemProperty.Name, dataItemProperty.GetValue(this, null).ToString());
            });
            return dictionary;
        }

        public DataItemController Clone()
        {
            return Factory.GetController(DataItem.Clone());
        }


        internal void ChangeInDb()
        {
            DataItem.ChangeInDb();
        }

        public bool Equals(DataItemController controller)
        {
            return (Id == controller.Id) && (IsTheSameByType(controller));
        }

        public abstract bool IsTheSameByType(DataItemController controller);


        
    }
}
