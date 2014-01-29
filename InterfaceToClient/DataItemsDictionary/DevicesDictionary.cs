using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InterfaceToDataBase;

namespace InterfaceToClient
{
    public class DevicesDictionary : DataItemControllersDictionary
    {
        public DevicesDictionary()
        { }
        protected override DataItemControllersFactory GetFactory()
        {
            return new DeviceControllersFactory();
        }
        public override List<DataItemController> GetPossibleParents(DataItemController dataItemController)
        {
            var deviceController = (DeviceController)dataItemController;
            if (!deviceController.HasType)
                return base.GetPossibleParents(dataItemController);
            if (!deviceController.Type.HasParentsWithoutMarker)
                return new List<DataItemController>();
            var parentListByParentTypeId = GetDevicesWithTypeId(deviceController.Type.ParentWithoutMarker.Id).ToList();
            GetDevicesWithTypeId(deviceController.Type.Id).Where(controller => controller.HasParents && parentListByParentTypeId.Contains(controller)).ToList()
                .ForEach(controller => parentListByParentTypeId.Remove(controller)); 
            return parentListByParentTypeId;
        }


        public List<DataItemController> GetDevicesWithCompanyId(int CompanyId)
        {
            return DataItemControllersDic.Values.Where(dataItemController => ((DeviceController)dataItemController).HasTheSameCompanyId(CompanyId)).ToList();
        }

        public IEnumerable<DataItemController> GetDevicesWithTypeId(int TypeId)
        {
            return DataItemControllersDic.Values.Where(dataItemController => ((DeviceController)dataItemController).HasTheSameTypeId(TypeId)).ToList();
        }

    }
}
