using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBActionLibrary;
using DataItemsLibrary;

namespace DataItemControllersLibrary
{
    public class DeviceEventController : DataItemController
    {
        public DeviceEventController(DeviceEvent deviceEvent, DeviceEventControllersFactory factory ) : base(deviceEvent , factory)
        { }
        private DeviceEvent Event { get { return (DeviceEvent)DataItem; } }
        private DevicesDictionary DevicesDic { get { return Factory.Vault.DevicesDic; } }

        const string _DeviceId = "DeviceId";
        const string _Type = "Type";
        const string _Date = "Date";
        protected override void OnPropertyChanged()
        {
            base.OnPropertyChanged();
            OnPropertyChanged(_Type);
            OnPropertyChanged(_Date);
            OnPropertyChanged(_DeviceId);
        }
        protected override bool IsTheSameByType(DataItemController controller)
        {
            return controller is DeviceEventController;
        }

        internal DeviceController Device { get { return (DeviceController)DevicesDic.DataItemControllersDic[Event.DeviceId]; } set { Event.DeviceId = value.Id; OnPropertyChanged(); } }
        internal DataItemAction GetActionForDeleteDeviceReference()
        {
            return new DataItemAction(dataItem, ActionType.DELETE);
        }

        public string Type { get { return Event.Type; } set { Event.Type = value; OnPropertyChanged(); } }
        public DateTime Date { get { return Event.Date; } set { Event.Date = value; } }
    }
}
