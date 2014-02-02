using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace InterfaceToDataBase
{
    public enum ActionType : byte
    {
        UPDATE = 0,
        INSERT = 1,
        DELETE = 2
    }
}
