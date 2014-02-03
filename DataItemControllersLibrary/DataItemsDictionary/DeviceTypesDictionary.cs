using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataItemsLibrary;

namespace DataItemControllersLibrary
{
    public class DeviceTypesDictionary : DataItemControllersWithParentsDictionary
    {
        public DeviceTypesDictionary(DictionariesVault vault) : base(vault)
        { }
        public DeviceTypesDictionary(DeviceTypeControllersFactory factory): base(factory)
        { }

        const string _UndefinedType = "Тип неопределен";
        private DeviceTypeController UndefinedType;
        private DeviceTypeController InitUndefinedType()
        {
            var controller = (DeviceTypeController)Factory.GetControllerEmpty();
            controller.Name = _UndefinedType;
            return controller;
        }

        protected override DataItemControllersFactory GetFactory(DictionariesVault vault)
        {
            return new DeviceTypeControllersFactory(vault);
        }

        internal DeviceTypeController GetUndefinedType()
        {
            if (UndefinedType == null)
                UndefinedType = InitUndefinedType();
            return UndefinedType;
        }
        internal IEnumerable<DataItemControllerWithParents> GetChildrenDevicesTypes(DeviceTypeController deviceTypeController)
        {
            var result = GetChildrenByParentId(deviceTypeController.Id).ToList();
            for (var i = 0; i < result.Count; i++)
            {
                var item = (DeviceTypeController)result[i];
                if (item.IsMarker)
                {
                    result.Remove(item);
                    i--;
                    result.AddRange(GetChildrenByParentId(item.Id));
                }
            }
            return result;
        }
        internal IEnumerable<DeviceTypeController> GetTypesWithoutMarker()
        {
            return DataItemControllersDic.Values.Where(dataItemController => !((DeviceTypeController)dataItemController).IsMarker).Cast<DeviceTypeController>();
        }
    }
}
