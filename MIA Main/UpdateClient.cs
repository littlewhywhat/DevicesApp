using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlTypes;
using System.Data.SqlClient;

namespace MiaMain
{
    public static class UpdateClient
    {
        static byte[] timestamp;
        public static void Do(SqlConnection connection)
        {
            var tempTimestamp = new GetTimestamp().Perform(connection);
            if (tempTimestamp != timestamp)
            {
                
            }
        }
    }
}
