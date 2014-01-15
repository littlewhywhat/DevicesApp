using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiaMain;

namespace MiaAppInterface
{
    public class DevicesController : DataItemsController
    {
        const string factoryName = "Devices";
        public DevicesController() : base(factoryName)
        { }
        protected override DataItemsGrid GetDataItemsGrid()
        {
            return new DevicesGrid(this);
        }

        protected override DataItem GetNewDataItem()
        {
            var device = Factory.GetDataItem() as Device;
            device.Name = "New device";
            device.Info = "New device";
            device.ParentId = 0;
            device.CompanyId = 0;
            device.Insert();
            return device;
        }

        protected override void GetDataItemToUpdate(DataItem dataItem, DataItemsGrid grid)
        {
            var deviceNew = dataItem as Device;
            var devicesGrid = grid as DevicesGrid;
            deviceNew.Name = devicesGrid.DeviceName.Text;
            deviceNew.Info = devicesGrid.DeviceInfo.Text;
            deviceNew.CompanyId = Convert.ToInt32(devicesGrid.DeviceCompanyId.Text);
            var selectedItem = ((Device)devicesGrid.DevicesParentComboBox.SelectedItem);
            deviceNew.ParentId = selectedItem == null ? 0 : selectedItem.Id;
        }
    }
}
