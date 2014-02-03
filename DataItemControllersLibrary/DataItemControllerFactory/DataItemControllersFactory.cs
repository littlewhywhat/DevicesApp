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
        public DataItemControllersFactory(DictionariesVault vault)
        {
            Vault = vault;
            Factory = GetFactory();
        }
        protected abstract DataItemsFactory GetFactory();
        protected DataItemsFactory Factory;

        internal DictionariesVault Vault { get; private set; }
        internal void FillDataItemControllersDic(DataItemControllersDictionary dataItemsDic)
        {
            dataItemsDic.Fill(Factory);
        }
        internal DataItemControllersDictionary RelatedDic { get { return Vault.GetDicByTableName(TableName); } }
        internal abstract DataItemController GetController(DataItem dataItem);
        internal DataItemController GetController(int id)
        {
            return GetController(Factory.GetFirstFilledDataItem(id));
        }
        internal DataItemController GetControllerEmpty()
        {
            return GetController(Factory.GetEmptyDataItem());
        }

        public string TableName { get { return Factory.TableName; } }
        public DataItemController GetControllerDefault()
        {
            return GetController(Factory.GetDataItemDefault());
        }
    }
}
