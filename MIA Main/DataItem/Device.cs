using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MiaMain
{
    public class Device : DataItem
    {
        public string FullName { get; set; }
        public int CompanyId { get; set; }
        public string ProductNumber { get; set; }
        public string IVUK { get; set; }
        public int TypeId { get; set; }
        public Device(DevicesFactory Factory) : base(Factory) { }

        public override bool IsTheSameType(DataItem dataItem)
        {
            return dataItem is Device;
        }
        
        

        public override Dictionary<string, string> GetSearchPropertyValueDic()
        {
            var searchPropertyValueDic = base.GetSearchPropertyValueDic();
            if (TypeId != 0)
            {
                var deviceType = FactoriesVault.FactoriesDic[TableNames.DeviceTypes].GetDataItemsDic()[TypeId];
                deviceType.GetSearchPropertyValueDic().ForEach(keyValuePair => searchPropertyValueDic.Add("Type" + keyValuePair.Key, keyValuePair.Value));
            }
            return searchPropertyValueDic;
        }
    }
}
