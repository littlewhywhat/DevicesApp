using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBActionLibrary;
using DataItemsLibrary;

namespace DataItemControllersLibrary
{
    public class DeviceTypeController : DataItemControllerWithParents
    {
        public DeviceTypeController(DeviceType dataItem, DeviceTypeControllersFactory factory) : base(dataItem, factory)
        { }
        private DevicesDictionary DevicesDic { get { return Factory.Vault.DevicesDic; } }
        private DeviceType DeviceType { get { return (DeviceType)DataItem; } }
        private DeviceTypeController DeviceTypeParentController { get { return HasParents ? (DeviceTypeController)Parent : null; } }
        private string GetMarker()
        {
            if (HasParents)
            {
                if (DeviceTypeParentController.IsMarker)
                    return DeviceTypeParentController.Name;
                else
                    return DeviceTypeParentController.GetMarker();
            }
            return _WithoutMarker;
        }
        private IEnumerable<DataItemAction> GetDevicesDeleteReferences()
        {
            return GetReferencedDevices().Select(deviceController => deviceController.GetActionForDeleteTypeReference());
        }
        private IEnumerable<DeviceController> GetReferencedDevices()
        {
            return DevicesDic.GetDevicesWithTypeId(DeviceType.Id);
        }

        const string _WithoutMarker = "Без маркера";
        const string _FullName = "FullName";
        const string _IVUK = "IVUK";
        const string _IsMarker = "IsMarker";
        const string _Marker = "Marker";
        protected override void OnPropertyChanged()
        {
            base.OnPropertyChanged();
            OnPropertyChanged(_FullName);
            OnPropertyChanged(_IsMarker);
            OnPropertyChanged(_IVUK);
            OnPropertyChanged(_Marker);
        }
        protected override bool IsTheSameByType(DataItemController controller)
        {
            return controller is DeviceTypeController;
        }
        protected override List<DataItemAction> GetDeleteReferencesActions()
        {
            var actions = base.GetDeleteReferencesActions();
            actions.AddRange(GetDevicesDeleteReferences());
            return actions;
        }
        protected override DataItemControllerWithParents Parent
        {
            get { return base.Parent; }
            set
            {
                base.Parent = value;
                OnPropertyChanged(_Marker);
            }
        }

        internal bool HasParentsWithoutMarker { get { return Parent != null; } }
        internal DataItemController ParentWithoutMarker
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
        public string Marker { get { return GetMarker(); } }
        public override void ChangeInDb()
        {
            if (ParentWasChanged)
                DeleteReferences();
            base.ChangeInDb();
        }
    }
}
