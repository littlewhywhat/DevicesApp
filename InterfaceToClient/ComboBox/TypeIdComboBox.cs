using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InterfaceToDataBase;

namespace InterfaceToClient
{
    public class TypeIdComboBox : DeviceExtraComboBox, Observer, IDisposable
    {
        public TypeIdComboBox() : base(TableNames.DeviceTypes)
        { }

        protected override DataItemController GetCurrentSelection()
        {
            return DeviceControllerDataContext.Type;
        }

        protected override IEnumerable<DataItemController> GetControllers()
        {
            return DeviceControllerDataContext.GetTypes();
        }

        protected override void ChangeCurrentDataContextBySelectedItem()
        {
            DeviceControllerDataContext.Type = (DeviceController)CurrentSelectedItem;
        }

    }
}
