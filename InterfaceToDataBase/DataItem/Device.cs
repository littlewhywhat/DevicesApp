using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace InterfaceToDataBase
{
    public class Device : DataItemWithParents
    {
        
        public Device(DevicesFactory Factory) : base(Factory) { }

        public int CompanyId { get; set; }
        public int TypeId { get; set; }

        public override bool IsTheSameType(DataItem dataItem)
        {
            return dataItem is Device;
        }
        
        

       
    }
}
