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
    }
}
