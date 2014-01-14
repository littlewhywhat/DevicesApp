using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiaMain;

namespace MiaAppInterface
{
    public abstract class DataItemsController
    {
        public abstract DataItemsFactory Factory { get; }
        public abstract DataItemsTabItem GetTabItem(DataItem dataItem);
        protected abstract DataItem GetNewDataItem();
        public abstract DataItemsTabItem GetTabItem();
    }
}
