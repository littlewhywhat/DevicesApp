using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaMain
{
    public class DBHelperDevices : DBHelper
    {
        public DBHelperDevices(string connectionString) : base(connectionString)
        {
            TableName = "Devices";
        }
        protected override DataItem GetDataItem()
        {
            return new Device();
        }
    }
}
