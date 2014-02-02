using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataItemsLibrary;

namespace DBActionLibrary
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
