using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InterfaceToDataBase;

namespace InterfaceToClient
{
    public class DeviceTypeController : DataItemController
    {
        public DeviceTypeController(DeviceType dataItem, DeviceTypeControllersFactory factory) : base(dataItem, factory)
        { }
        protected override DataItemControllersDictionary GetDictionary()
        {
            return FactoriesVault.Dic[Factory.TableName];
        }
        public DataItemController ParentWithoutMarker
                {
                    get
                    {
                        if (HasParents)
                        {
                            var parent = (DeviceTypeController)Parent;
                            if (parent.IsMarker)
                                return parent.Parent;
                            return parent;
                        }
                        return Parent;
                    }
                }

        public DeviceType DeviceType { get { return (DeviceType)DataItem; } }

        const string _FullName = "FullName";
        const string _IVUK = "IVUK";
        const string _IsMarker = "IsMarker";
        protected override void OnPropertyChanged()
        {
            base.OnPropertyChanged();
            OnPropertyChanged(_FullName);
            OnPropertyChanged(_IsMarker);
            OnPropertyChanged(_IVUK);
        }

        public string FullName
        {
            get { return DeviceType.FullName; }
            set
            {
                DeviceType.FullName = value;
                OnPropertyChanged();
            }
        }
        public string IVUK
        {
            get { return DeviceType.IVUK; }
            set
            {
                DeviceType.IVUK = value;
                OnPropertyChanged();
            }
        }
        public bool IsMarker
        {
            get { return DeviceType.IsMarker; }
            set
            {
                DeviceType.IsMarker = value;
                OnPropertyChanged();
            }
        }
        public bool HasParentsWithoutMarker { get { return Parent != null; } }


        public DeviceTypeController DeviceTypeParentController { get { return HasParents? (DeviceTypeController)Parent : null; } }
        public string Marker { get { return GetMarker(); } }
        
        private string GetMarker()
        {
            if (HasParents)
            {
                if (DeviceTypeParentController.IsMarker)
                    return DeviceTypeParentController.Name;
                else
                    return DeviceTypeParentController.GetMarker();
            }
            return "Без маркера";
        }

        protected override List<TransactionData> GetDeleteReferencesActions()
        {
            var actions = base.GetDeleteReferencesActions();
            actions.AddRange(((DevicesDictionary)FactoriesVault.Dic[TableNames.Devices]).GetDevicesWithTypeId(DeviceType.Id)
                .Select(deviceController => (TransactionData)((DeviceController)deviceController).DeleteTypeTransaction()));
            return actions;
        }

        public override bool IsTheSameByType(DataItemController controller)
        {
            return controller is DeviceTypeController;
        }

    }
}
