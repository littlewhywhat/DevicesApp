using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Data.Common;
using DataItemsLibrary;

namespace DBActionLibrary
{
    internal interface DBAction
    {
        object Act(DbConnection connection);
    }
}
