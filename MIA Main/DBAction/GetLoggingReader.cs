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
            var command = GetCommand();
            command.Connection = connection;
            return command.ExecuteReader();
        }

        private SqlCommand GetCommand()
        {
            var timestamp = new SqlParameter("@oldTimestamp", SqlDbType.Timestamp);
            timestamp.Value = OldTimestamp;
            var command = new SqlCommand("SELECT * FROM Logging WHERE Timestamp > @oldTimestamp");
            command.Parameters.Add(timestamp);
            return command;
        }
    }
}
