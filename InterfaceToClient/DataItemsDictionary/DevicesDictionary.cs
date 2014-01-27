using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InterfaceToDataBase;

namespace InterfaceToClient
{
    public class DevicesDictionary : DataItemsDictionary
    {
        public DevicesDictionary(DevicesFactory factory) : base(factory)
        { }

        public List<DataItem> GetDevicesWithCompanyId(int CompanyId)
        {
            return DataItemsDic.Values.Where(dataItem => ((Device)dataItem).CompanyId == CompanyId).ToList();
        }

        public List<DataItem> GetDevicesWithTypeId(int TypeId)
        {
            return DataItemsDic.Values.Where(dataItem => ((Device)dataItem).TypeId == TypeId).ToList();
        }
    }
}
