using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataItemsLibrary
{
    public abstract class DataItemWithParents : DataItem
    {
        public DataItemWithParents(DataItemsFactory factory) : base(factory) { }
        public int ParentId { get; set; }
    }
}
