using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;

namespace MiaMain
{
    public interface Observer
    {
        void Add(DataItem dataItemNew);
        void Remove(DataItem dataItemOld);
        void Replace(DataItem dataItemNew, DataItem dataItemOld);
    }
}
