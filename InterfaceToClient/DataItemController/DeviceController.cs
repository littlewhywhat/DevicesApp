using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InterfaceToDataBase;

namespace InterfaceToClient
{
    public class DeviceController : DataItemController
    {
        public DeviceController(DataItem dataItem, DataItemControllersFactory factory) : base(dataItem, factory) { }

        private DeviceTypeControllersFactory DeviceTypeControllersFactory { get { return (DeviceTypeControllersFactory)FactoriesVault.Dic[TableNames.DeviceTypes]; } }
        private Device Device { get { return (Device)DataItem; } }

        
        
        public DataItemController Type
        {
            get { return null; }
            set { Device.TypeId = value.Id;}
        }
        public DataItemController Company
        {
            get { return null; }
            set { Device.CompanyId = value.Id; }
        }

        public IEnumerable<DataItemController> GetTypes()
        {
            return null;
        }

        public IEnumerable<DataItemController> GetCompanies()
        {
            return null;
        }

        public override Dictionary<string, string> GetSearchPropertyValueDic()
        {
            var searchPropertyValueDic = base.GetSearchPropertyValueDic();
            if (Device.TypeId != 0)
                Type.GetSearchPropertyValueDic().ForEach(keyValuePair => searchPropertyValueDic.Add("Type" + keyValuePair.Key, keyValuePair.Value));
            return searchPropertyValueDic;
        }

        public IEnumerable<DataItemController> GetEvents()
        {
            return null;
            //Factory.DataItemsDic.Select(keyValuePair => (DeviceEvent)keyValuePair.Value).
              //  Where(deviceEvent => deviceEvent.DeviceId == Device.Id).
        }

        public override bool IsTheSameByType(DataItemController controller)
        {
            return controller is DeviceController;
        }
    }
}
