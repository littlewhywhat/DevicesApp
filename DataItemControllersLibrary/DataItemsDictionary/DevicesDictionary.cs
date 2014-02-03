using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataItemsLibrary;

namespace DataItemControllersLibrary
{
    public class DevicesDictionary : DataItemControllersWithParentsDictionary
    {
        public DevicesDictionary(DictionariesVault vault) : base(vault)
        { }
        public DevicesDictionary(DeviceControllersFactory factory) : base(factory)
        { }

        protected override DataItemControllersFactory GetFactory(DictionariesVault vault)
        {
            return new DeviceControllersFactory(vault);
        }

        internal override IEnumerable<DataItemControllerWithParents> GetPossibleParents(DataItemControllerWithParents dataItemController)
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
        internal IEnumerable<DeviceController> GetDevicesWithCompanyId(int CompanyId)
        {
            return DataItemControllersDic.Values.Where(dataItemController => ((DeviceController)dataItemController).HasTheSameCompanyId(CompanyId))
                .Cast<DeviceController>();
        }
        internal IEnumerable<DeviceController> GetDevicesWithTypeId(int TypeId)
        {
            return DataItemControllersDic.Values.Where(dataItemController => ((DeviceController)dataItemController).HasTheSameTypeId(TypeId))
                .Cast<DeviceController>();
        }

    }
}
