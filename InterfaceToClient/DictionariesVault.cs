using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataItemsLibrary;
using DBActionLibrary;
using DataItemControllersLibrary;

namespace InterfaceToClient
{
    public class DictionariesVault : DataItemControllersLibrary.DictionariesVault
    {
        public DicChangesGetter ChangesGetter { get; private set; }
        protected override void InitDictionariesVault()
        {
            Dic.Add(TableNames.Devices, new DevicesDictionary(new ExtDeviceControllersFactory(this)));
            Dic.Add(TableNames.DeviceTypes, new DeviceTypesDictionary(new ExtDeviceTypeControllersFactory(this)));
            Dic.Add(TableNames.DeviceEvents, new DeviceEventsDictionary(new ExtDeviceEventControllersFactory(this)));
            Dic.Add(TableNames.Companies, new CompaniesDictionary(new ExtCompanyControllersFactory(this)));
            ChangesGetter = new DicChangesGetter(this);
        }
    }
}
