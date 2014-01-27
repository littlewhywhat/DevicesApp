using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;

namespace InterfaceToClient
{
    public interface Observer
    {
        void Update(DataItemControllerChangedEventArgs change);
        void Dispose();
    }
}
