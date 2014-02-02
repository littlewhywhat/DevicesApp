using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBActionLibrary;
using DataItemsLibrary;

namespace InterfaceToClient
{
    public class DeviceEventController : DataItemController
    {
        public DeviceEventController(DeviceEvent deviceEvent, DeviceEventControllersFactory factory ) : base(deviceEvent , factory)
        { }
        protected override DataItemControllersDictionary GetDictionary()
        {
            return FactoriesVault.Dic[Factory.TableName];
        }

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
        public DeviceEvent Event { get { return (DeviceEvent)DataItem; } }
        public DevicesDictionary DevicesDic { get { return (DevicesDictionary)FactoriesVault.Dic[TableNames.Devices]; } }


        public DeviceController Device { get { return (DeviceController)DevicesDic.DataItemControllersDic[Event.DeviceId]; } set { Event.DeviceId = value.Id; OnPropertyChanged(); } }
        public string Type { get { return Event.Type; } set { Event.Type = value; OnPropertyChanged(); } }
        public DateTime Date { get { return Event.Date; } set { Event.Date = value; } }
        


        public override bool IsTheSameByType(DataItemController controller)
        {
            return controller is DeviceEventController;
        }

        internal DataItemAction GetActionForDeleteDeviceReference()
        {
            return new DataItemAction(dataItem, ActionType.DELETE);
        }
    }
}
