using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;

namespace DataItemsLibrary
{
    public class DeviceEvent : DataItem
    {
        public DeviceEvent(DeviceEventsFactory Factory) : base(Factory) { }
        public int DeviceId { get; set; }
        public string Type { get; set; }
        public DateTime Date { get; set; }
        public override bool IsTheSameType(DataItem dataItem)
        {
            return dataItem is DeviceEvent;
        }
    }
}
