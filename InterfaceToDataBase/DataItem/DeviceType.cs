using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterfaceToDataBase
{
    public class DeviceType : DataItem
    {
        public DeviceType(DeviceTypesFactory Factory):base(Factory)
        { }

        public bool IsMarker { get; set; }

        public override bool IsTheSameType(DataItem dataItem)
        {
            return dataItem is DeviceType;
        }
    }
}
