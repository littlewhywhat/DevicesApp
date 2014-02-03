using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataItemsLibrary;
using DataItemControllersLibrary;

namespace InterfaceToClient
{
    public abstract class DeviceExtraComboBox : DataItemsComboBox, IObserver
    {
        protected DeviceController DeviceControllerDataContext { get { return (DeviceController)DataContext; } }
        public DeviceExtraComboBox(string TableName) : base()
        {
            Vault.ChangesGetter.AddObserver(this, new string[] { TableName });
        }

        public void Update(DataItemControllerChangedEventArgs change)
        {
            try { DeviceControllerDataContext.Refresh(); }
            catch (Exception) { }
        }

        public void Dispose()
        {
            Vault.ChangesGetter.RemoveObserver(this);
            this.DisposeChildrenObservers();
        }
    }
}
