using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterfaceToDataBase
{
    public class DataItemAction
    {
        public ActionType Action { get; set; }
        public DataItem DataItem { get; set; }
        public DataItemAction(DataItem dataItem, ActionType action)
        {
            Action = action;
            DataItem = dataItem;
        }
    }
}
