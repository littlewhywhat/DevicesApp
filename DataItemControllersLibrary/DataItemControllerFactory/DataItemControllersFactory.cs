using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using DataItemsLibrary;
using System.Windows;
using DBActionLibrary;

namespace DataItemControllersLibrary
{
    public abstract class DataItemControllersFactory
    {
        public DictionariesVault Vault { get; private set; }
        protected DataItemsFactory Factory;
        public DataItemControllersFactory(DictionariesVault vault)
        {
            Vault = vault;
            Factory = GetFactory();
        }
        protected abstract DataItemsFactory GetFactory();
        
        public void FillDataItemControllersDic(DataItemControllersDictionary dataItemsDic)
        {
            dataItemsDic.Fill(Factory);
        }
        public string TableName { get { return Factory.TableName; } }
        public DataItemControllersDictionary RelatedDic { get { return Vault.GetDicByTableName(TableName); } }
        public abstract DataItemController GetController(DataItem dataItem);
        public DataItemController GetController(int id)
        {
            return GetController(Factory.GetFirstFilledDataItem(id));
        }
        public DataItemController GetControllerEmpty()
        {
            return GetController(Factory.GetEmptyDataItem());
        }
        public DataItemController GetControllerDefault()
        {
            return GetController(Factory.GetDataItemDefault());
        }
    }
}
