using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InterfaceToDataBase;

namespace InterfaceToClient
{
    public class DeviceTypeController : DataItemController
    {
        public DeviceTypeController(DeviceType dataItem, DeviceTypeControllersFactory factory) : base(dataItem, factory)
        { }

        public DeviceType DeviceType { get { return (DeviceType)DataItem; } }
        public DeviceType DeviceTypeParent { get { return (DeviceType)DataItemParent; } }
        public DeviceTypeController DeviceTypeParentController { get { return (DeviceTypeController)Parent; } }
        public string Marker { get { return GetMarker(); } }
        public bool IsMarker { get { return DeviceType.IsMarker; } }
        private string GetMarker()
        {
            if (HasParents)
            {
                var parentType = Factory.GetDataItemController(DataItem.ParentId);
                if (DeviceTypeParentController.IsMarker)
                    return DeviceTypeParent.Name;
                else
                    return DeviceTypeParentController.GetMarker();
            }
            return "Без маркера";
        }

        protected override List<TransactionData> GetDeleteReferencesActions()
        {
            var actions = base.GetDeleteReferencesActions();
            actions.AddRange(((DeviceControllersFactory)FactoriesVault.Dic[TableNames.Devices]).GetDevicesWithTypeId(DeviceType.Id).Select(device =>
            {
                ((Device)device).TypeId = 0;
                return (TransactionData)new UpdateDataItem(device);
            }));
            return actions;
        }

        public override bool IsTheSameByType(DataItemController controller)
        {
            return controller is DeviceTypeController;
        }
    }
}
