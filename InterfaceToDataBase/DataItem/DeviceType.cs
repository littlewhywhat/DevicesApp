using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterfaceToDataBase
{
    public class DeviceType : DataItemWithParents
    {
        public DeviceType(DeviceTypesFactory Factory):base(Factory)
        { }

        public bool IsMarker { get; set; }
        public string FullName { get; set; }
        public string IVUK { get; set; }

        public override bool IsTheSameType(DataItem dataItem)
        {
            return dataItem is DeviceType;
        }
    }
}
