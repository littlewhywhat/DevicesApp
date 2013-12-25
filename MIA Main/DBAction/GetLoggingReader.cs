using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data;

namespace MiaMain
{
    public class GetLoggingReader : DBAction
    {
        byte[] OldTimestamp;
        public GetLoggingReader(byte[] oldTimestamp)
        {
            OldTimestamp = oldTimestamp;
        }
        public object Act(SqlConnection connection)
        {
            return GetCommand(connection).ExecuteReader();
        }

        private SqlCommand GetCommand(SqlConnection connection)
        {
            var command = new SqlCommand("SELECT * FROM Logging WHERE Timestamp > @oldTimestamp", connection);
            command.Parameters.Add(new SqlParameter("@oldTimestamp", SqlDbType.Timestamp) { Value = OldTimestamp });
            return command;
        }
    }
}
