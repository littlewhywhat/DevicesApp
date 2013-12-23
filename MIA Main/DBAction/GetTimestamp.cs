using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace MiaMain
{
    public class GetTimestamp : DBAction
    {
        
        protected override object Act(SqlConnection connection)
        {
            return new SqlCommand(GetCommandText(), connection).ExecuteScalar();
        }

        private string GetCommandText()
        {
            return "SELECT MAX(Timestamp) FROM Logging";
        }
    }
}
