using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using DataItemsLibrary;

namespace DBActionLibrary
{
    public enum ActionType : byte
    {
        UPDATE = 0,
        INSERT = 1,
        DELETE = 2
    }
}
