﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataItemsLibrary;

namespace InterfaceToClient
{
    public class DeviceTypesDictionary : DataItemControllersWithParentsDictionary
    {
        private DeviceTypeController UndefinedType;
        public DeviceTypesDictionary()
        { }


        protected override DataItemControllersFactory GetFactory()
        {
            return new DeviceTypeControllersFactory();
        }

        public IEnumerable<DeviceTypeController> GetTypesWithoutMarker()
        {
            return DataItemControllersDic.Values.Where(dataItemController => !((DeviceTypeController)dataItemController).IsMarker).Cast<DeviceTypeController>();
        }

        const string _UndefinedType = "Тип неопределен";
        private DeviceTypeController InitUndefinedType()
        {
            var controller = (DeviceTypeController)Factory.GetControllerEmpty();
            controller.Name = _UndefinedType;
            return controller;
        }

        public DeviceTypeController GetUndefinedType()
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
    }
}
