using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using InterfaceToDataBase;

namespace InterfaceToClient
{
    public class DeviceControllersFactory : DataItemControllersFactory
    {
        public DeviceControllersFactory(DevicesFactory factory) : base(factory)
        { }

        private DevicesDictionary DevicesDic { get { return (DevicesDictionary)DataItemsDic; } }

        public override DataItemController GetController(DataItem dataItem)
        {
            return new DeviceController(dataItem, this);
        }

        public List<DataItem> GetDevicesWithCompanyId(int CompanyId)
        {
            return DevicesDic.GetDevicesWithCompanyId(CompanyId);                
        }

        public List<DataItem> GetDevicesWithTypeId(int TypeId)
        {
            return DevicesDic.GetDevicesWithTypeId(TypeId);
        }

        protected override DataItemsDictionary GetDataItemsDictionary(DataItemsFactory factory)
        {
            return new DevicesDictionary((DevicesFactory)factory);
        }

        protected override Grid GetTabItemContent()
        {
            return new DevicesGrid();
        }
    }
}
