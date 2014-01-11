﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace MiaMain
{
    public class UpdateDataItem : ChangeData
    {
        public UpdateDataItem(DataItem dataItem) : base(dataItem, ActionType.UPDATE)
        { }

        protected override string GetMainCommandText()
        {
            return String.Format("UPDATE {0} Set {1} where Id = {2}", dataItem.Factory.TableName, 
                String.Join(",", dataItem.GetPropertyValueDic().Select(pair => String.Format("{0}='{1}'", pair.Key, pair.Value))),
                dataItem.Id);
        }
    }
}
