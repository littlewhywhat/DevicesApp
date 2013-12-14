using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIA_Main
{
    class Device
    {
        public int Id { get; private set; }
        public string Info { get; private set; }
        public Device(int id, string info)
        {
            Id = id;
            Info = info;
        }
    }
}
