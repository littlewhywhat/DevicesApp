using System;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace InterfaceToClient
{
    public class DataItemControllerChangedEventArgs : EventArgs
    {
        public DataItemController NewController { get; set; }
        public DataItemController OldController { get; set; }
        public NotifyCollectionChangedAction Action { get; set; }
        public string TableName { get; set; }
    }
}
