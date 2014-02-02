using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataItemsLibrary;

namespace InterfaceToClient
{
    public class DevicesDictionary : DataItemControllersWithParentsDictionary
    {
        public DevicesDictionary()
        { }
        protected override DataItemControllersFactory GetFactory()
        {
            return new DeviceControllersFactory();
        }
        public override IEnumerable<DataItemControllerWithParents> GetPossibleParents(DataItemControllerWithParents dataItemController)
        {
            var deviceController = (DeviceController)dataItemController;
            if (!deviceController.HasType)
                return base.GetPossibleParents(dataItemController);
            if (!deviceController.Type.HasParentsWithoutMarker)
                return new List<DataItemControllerWithParents>();
            var parentListByParentTypeId = GetDevicesWithTypeId(deviceController.Type.ParentWithoutMarker.Id).ToList();
            GetDevicesWithTypeId(deviceController.Type.Id).Where(controller => controller.HasParents && parentListByParentTypeId.Contains(controller)).ToList()
                .ForEach(controller => parentListByParentTypeId.Remove(controller)); 
            return parentListByParentTypeId.Cast<DataItemControllerWithParents>();
        }


        public IEnumerable<DeviceController> GetDevicesWithCompanyId(int CompanyId)
        {
            return DataItemControllersDic.Values.Where(dataItemController => ((DeviceController)dataItemController).HasTheSameCompanyId(CompanyId))
                .Cast<DeviceController>();
        }

        public IEnumerable<DeviceController> GetDevicesWithTypeId(int TypeId)
        {
            return DataItemControllersDic.Values.Where(dataItemController => ((DeviceController)dataItemController).HasTheSameTypeId(TypeId))
                .Cast<DeviceController>();
        }

    }
}
