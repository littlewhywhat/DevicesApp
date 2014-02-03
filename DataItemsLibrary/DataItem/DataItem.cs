using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace DataItemsLibrary
{
    public abstract class DataItem
    {
        public DataItemsFactory Factory { get; private set; }
        public int Id { get; set; }
        public string Name { get; set; }

        public DataItem(DataItemsFactory factory)
        {
            Factory = factory;
        }
        public bool Equals(DataItem dataItem)
        {
            return (IsTheSameType(dataItem) && Id == dataItem.Id);
        }
        public abstract bool IsTheSameType(DataItem dataItem);


    }
}
