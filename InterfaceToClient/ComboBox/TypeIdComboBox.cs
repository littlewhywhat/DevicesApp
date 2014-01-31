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

        //protected override DataItemController GetCurrentSelection()
        //{
        //    return DeviceControllerDataContext.Type;
        //}

        //protected override IEnumerable<DataItemController> GetControllersWithoutEmpty()
        //{
        //    return DeviceControllerDataContext.GetTypes();
        //}

        //protected override void ChangeCurrentDataContextBySelectedItem()
        //{
        //    DeviceControllerDataContext.Type = (DeviceTypeController)CurrentSelectedItem;
        //}

        //protected override DataItemController GetEmptyController()
        //{
        //    var controller = FactoriesVault.Dic[TableNames.DeviceTypes].Factory.GetControllerEmpty();
        //    controller.Name = "Тип не определен";
        //    return controller;
        //}
    }
}
