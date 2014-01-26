using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiaMain
{
    public class DeviceTypesFactory : DataItemsFactory
    {
        private List<string> firstTableFields = new List<String> { "Id", "Name", "ParentId", "IsMarker"};
        private List<string> otherTableFields = new List<String> { "Name"};
        private List<string> searchTableFields = new List<String> { "Name" };

        public override List<string> FirstTableFields { get { return firstTableFields; } }
        public override List<string> OtherTableFields { get { return otherTableFields; } }
        public override List<string> SearchTableFields { get { return searchTableFields; } }
        public override string TableName { get { return TableNames.DeviceTypes; } }

        public override DataItem GetEmptyDataItem()
        {
            return new DeviceType(this);
        }

        protected override void DeleteReferences(DataItem dataItem, System.Data.Common.DbTransaction transaction)
        {
            FactoriesVault.FactoriesDic[TableNames.Devices].GetDataItemsDic().Values.ForEach(device =>
            {
                if (((Device)device).TypeId == dataItem.Id)
                {
                    ((Device)device).TypeId = 0;
                    new UpdateDataItem(device).PerformTransaction(transaction);
                }
            });
            base.DeleteReferences(dataItem, transaction);
        }
    }
}
