using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataItemsLibrary;

namespace InterfaceToClient
{
    public class TypeIdComboBox : DeviceExtraComboBox, IObserver, IDisposable
    {
        public TypeIdComboBox() : base(TableNames.DeviceTypes)
        { }
    }
}
