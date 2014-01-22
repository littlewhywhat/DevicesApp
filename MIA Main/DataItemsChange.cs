using System;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace MiaMain
{
    public class DataItemsChange
    {
        public DataItem NewDataItem { get; set; }
        public DataItem OldDataItem { get; set; }
        public NotifyCollectionChangedAction Action { get; set; }
        public DataItemsChange() { }

    }
}
