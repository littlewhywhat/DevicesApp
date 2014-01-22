using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using MiaMain;

namespace MiaAppInterface
{
    public class DevicesController : DataItemsController
    {
        const string factoryName = "Devices";
        public DevicesController() : base(factoryName)
        { }

        protected override Grid GetTabItemContent()
        {
            return new DevicesGrid();
        }
    }
}
