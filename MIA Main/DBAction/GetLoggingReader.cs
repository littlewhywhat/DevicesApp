using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
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
        public object Act(DbConnection connection)
        {
            return GetLogRow(GetCommand(connection).ExecuteReader());
        }
        private List<LogRow> GetLogRow(DbDataReader reader)
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

        private DbCommand GetCommand(DbConnection connection)
        {
            var command = Connection.GetCommand("SELECT * FROM Logging WHERE Timestamp > @oldTimestamp", connection);

            command.Parameters.Add(Connection.GetTimestampParameter("@oldTimestamp", OldTimestamp ));
            return command;
        }
    }
}
