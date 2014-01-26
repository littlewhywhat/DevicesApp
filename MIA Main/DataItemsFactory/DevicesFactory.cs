using System;
using System.Collections.Generic;
using System.Linq;

namespace MiaMain
{
    public class DevicesFactory : DataItemsFactory
    {
        private List<string> firstTableFields = new List<String> { "Id", "Name", "ParentId", "CompanyId", "FullName", "IVUK", "ProductNumber", "TypeId" };
        private List<string> otherTableFields = new List<String> { "FullName" };
        private List<string> searchTableFields = new List<String> { "Name", "FullName", "IVUK", "ProductNumber" };
        
        public override List<string> FirstTableFields { get { return firstTableFields; } }
        public override List<string> OtherTableFields { get { return otherTableFields; } }
        public override List<string> SearchTableFields { get { return searchTableFields; } }
        public override string TableName { get { return TableNames.Devices; } }

        public override DataItem GetEmptyDataItem()
        {
            return new Device(this);
        }

        protected override void DeleteReferences(DataItem dataItem, System.Data.Common.DbTransaction transaction)
        {
            FactoriesVault.FactoriesDic[TableNames.DeviceEvents].GetDataItemsDic().Values.ForEach(deviceEvent =>
                {
                    if (((DeviceEvent)deviceEvent).DeviceId == dataItem.Id)
                        deviceEvent.Delete();
                });
            base.DeleteReferences(dataItem, transaction);
        }
    }
}
