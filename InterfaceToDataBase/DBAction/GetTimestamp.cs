using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Data.Common;

namespace InterfaceToDataBase
{
    internal class GetTimestamp : DBAction
    {
        
        public object Act(DbConnection connection)
        {
            return Connection.GetCommand(GetCommandText(), connection).ExecuteScalar();
        }

        private string GetCommandText()
        {
            return "SELECT MAX(Timestamp) FROM Logging";
        }
    }
}
