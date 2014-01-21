using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiaMain
{
    public class EventsFactory : DataItemsFactory
    {
        private List<string> firstTableFields = new List<String> { "Id", "Name", "ParentId", "CompanyId" };
        private List<string> otherTableFields = new List<String> { "FullName", "IVUK", "ProductNumber" };
        private string tableName = "Devices";

        public override List<string> FirstTableFields
        {
            get { throw new NotImplementedException(); }
        }

        public override List<string> OtherTableFields
        {
            get { throw new NotImplementedException(); }
        }

        public override string TableName
        {
            get { throw new NotImplementedException(); }
        }

        public override DataItem GetEmptyDataItem()
        {
            throw new NotImplementedException();
        }
    }
}
