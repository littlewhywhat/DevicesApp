﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace MiaMain
{
    public abstract class DataItemsFactory
    {
        private Dictionary<int, DataItem> DataItemsDic = new Dictionary<int, DataItem>();
        public abstract List<String> FirstTableFields { get; }
        public abstract List<String> OtherTableFields { get; }
        public abstract string TableName { get; }
        private SqlConnection Connection { get; set; }
        public DataItemsFactory(SqlConnection connection)
        {
            Connection = connection;
        }
        public Dictionary<int, DataItem> GetDataItemsDic()
        {
            return DataItemsDic;
        }
        public abstract DataItem GetDataItem();
        //public void FillDataItem(DataItem dataItem)
        //{
        //    DBHelper.PerformDBAction(Connection, new FillDataItem(dataItem, ));
        //}
    }
}
