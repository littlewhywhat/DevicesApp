using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using MiaMain;

namespace MiaAppInterface
{
    public class EventsController : DataItemsController
    {
        int DeviceId { get; set; }
        const string FactoryName = "DeviceEvents";
        public EventsController(int deviceId): base(FactoryName)
        { 
            DeviceId = deviceId;
        }
        protected override Grid GetTabItemContent() 
        {
            var dataGrid = new ManagerGrid();
            dataGrid.contentGrid.Children.Add(new DeviceEventGrid());
            return dataGrid;
        }

        public override DataItem GetNewDataItem()
        {
            var dataItem = (DeviceEvent)Factory.GetEmptyDataItem();
            dataItem.DeviceId = DeviceId;
            dataItem.Name = "Новое событие";
            return dataItem;
        }
    }
}
