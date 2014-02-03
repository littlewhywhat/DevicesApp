using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataItemsLibrary;
using DBActionLibrary;
namespace DataItemControllersLibrary
{
    public class DictionariesVault
    {
        protected virtual void InitDictionariesVault()
        {
            Dic.Add(TableNames.Devices, new DevicesDictionary(this));
            Dic.Add(TableNames.DeviceTypes, new DeviceTypesDictionary(this));
            Dic.Add(TableNames.DeviceEvents, new DeviceEventsDictionary(this));
            Dic.Add(TableNames.Companies, new CompaniesDictionary(this));
        }

        internal DevicesDictionary DevicesDic { get { return (DevicesDictionary)Dic[TableNames.Devices]; } }
        internal DeviceTypesDictionary DeviceTypesDic { get { return (DeviceTypesDictionary)Dic[TableNames.DeviceTypes]; } }
        internal DeviceEventsDictionary DeviceEventsDic { get { return (DeviceEventsDictionary)Dic[TableNames.DeviceEvents]; } }
        internal CompaniesDictionary CompaniesDic { get { return (CompaniesDictionary)Dic[TableNames.Companies]; } }

        public Dictionary<string, DataItemControllersDictionary> Dic = new Dictionary<string, DataItemControllersDictionary>();
        public void StartWorking()
        {
            UpdateClient.SetVault(this);
            UpdateClient.SetTimestamp(Connection.GetLogTimestamp());
            InitDictionariesVault();
            UpdateClient.Control();
        }
        public DataItemControllersDictionary GetDicByTableName(string TableName)
        {
            return Dic[TableName];
        }
    }
}
