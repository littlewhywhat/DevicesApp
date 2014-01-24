using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Common;

namespace MiaMain
{
    public class CompaniesFactory : DataItemsFactory
    {
        private List<string> firstTableFields = new List<String> { "Id", "Name", "ParentId", "Info" };
        private List<string> otherTableFields = new List<String> { "Info" };
        private List<string> searchTableFields = new List<String> { "Info", "Name" };
        public override List<string> FirstTableFields { get { return firstTableFields; } }
        public override List<string> OtherTableFields { get { return otherTableFields; } }
        public override List<string> SearchTableFields { get { return searchTableFields; } }
        public override string TableName { get { return TableNames.Companies; } }
        
        public override DataItem GetEmptyDataItem()
        {
            return new Company(this);
        }
        protected override void DeleteReferences(DataItem dataItem, DbTransaction transaction)
        {
            base.DeleteReferences(dataItem, transaction);
            var devicesDic = FactoriesVault.FactoriesDic[TableNames.Devices].GetDataItemsDic();
            devicesDic.Select(keyValuePair => (Device)keyValuePair.Value).
                Where(device => device.CompanyId == dataItem.Id).ForEach(device =>
                {
                    new FillDataItem(device, device.Factory.OtherTableFields).Act(transaction.Connection);
                    device.CompanyId = 0;
                    new UpdateDataItem(device).PerformTransaction(transaction);
                });
        }

    }
}
