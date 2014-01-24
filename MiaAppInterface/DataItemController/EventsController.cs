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
        
        public EventsController(int deviceId): base(TableNames.DeviceEvents)
        { 
            DeviceId = deviceId;
        }
        protected override Grid GetTabItemContent() 
        {
            var dataGrid = new TabItemGrid();
            dataGrid.ContentGrid = new DeviceEventGrid();
            return dataGrid;
        }



        public override DataItem GetNewDataItem()
        {
            var dataItem = (DeviceEvent)Factory.GetEmptyDataItem();
            dataItem.DeviceId = DeviceId;
            dataItem.Name = "Новое событие";
            dataItem.Date = DateTime.Now;
            return dataItem;
        }
    }
}
