using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InterfaceToDataBase;

namespace InterfaceToClient
{
    public abstract class DeviceExtraComboBox : DataItemsComboBox, Observer
    {
        protected DeviceController DeviceControllerDataContext { get { return (DeviceController)DataContext; } }
        public DeviceExtraComboBox(string TableName) : base()
        {
            FactoriesVault.ChangesGetter.AddObserver(this, new string[] { TableName });
        }

        public void Update(DataItemControllerChangedEventArgs change)
        {
            try { DeviceControllerDataContext.Refresh(); }
            catch (Exception) { }
        }

        public void Dispose()
        {
            FactoriesVault.ChangesGetter.RemoveObserver(this);
            this.DisposeChildrenObservers();
        }
    }
}
