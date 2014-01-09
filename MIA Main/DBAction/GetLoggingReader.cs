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

            return GetLogRow(GetCommand(connection).ExecuteReader());
        }
        private List<LogRow> GetLogRow(SqlDataReader reader)
        {
            var list = new List<LogRow>();
            while(reader.Read())
            {
                var logRow = new LogRow();
                logRow.GetType().GetProperties().ForEach(property => property.SetValue(logRow, Convert.ChangeType(reader[property.Name], property.PropertyType)));
                list.Add(logRow);
            }
            return list;
        }

        private SqlCommand GetCommand(SqlConnection connection)
        {
            var command = new SqlCommand("SELECT * FROM Logging WHERE Timestamp > @oldTimestamp", connection);
            command.Parameters.Add(new SqlParameter("@oldTimestamp", SqlDbType.Timestamp) { Value = OldTimestamp });
            return command;
        }
    }
}
