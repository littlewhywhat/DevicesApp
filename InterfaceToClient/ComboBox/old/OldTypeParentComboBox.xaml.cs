using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.Specialized;
using InterfaceToDataBase;

namespace InterfaceToClient
{
    /// <summary>
    /// Логика взаимодействия для TypeParentComboBox.xaml
    /// </summary>
    public partial class OldTypeParentComboBox : ComboBox
    {
        public OldTypeParentComboBox() : base()
        {
        }

        private void ComboBox_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext != null)
            {
                //var dataItem = (Device)DataContext;
                //var parentsList = new List<DataItem>() { GetWithoutParentItem(dataItem.Factory) };
                //parentsList.AddRange(dataItem.TypeId != 0 ? GetParentsByType(dataItem) : GetParents(dataItem));
                //DataItem currentParrent = dataItem.ParentId != 0 ?
                //    dataItem.Factory.DataItemsDic[dataItem.ParentId] : parentsList[0];
                //ItemsSource = parentsList;
                //if (parentsList.Contains(currentParrent))
                //    SelectedItem = currentParrent;
                //else
                //    SelectedIndex = 0;
            }
        }

        private List<DataItem> GetParentsByType(Device device)
        {
            //var deviceTypesDic = FactoriesVault.FactoriesDic[TableNames.DeviceTypes].DataItemsDic;
            //var devicesDic = FactoriesVault.FactoriesDic[TableNames.Devices].DataItemsDic;
            //var deviceType = deviceTypesDic[device.TypeId];
            //var parentType = GetParentType(deviceType);
            //if (parentType == null)
            //    return new List<DataItem>();
            //var parentListByParentTypeId = devicesDic.Values.Where(parentDevice =>
            //    ((parentDevice.Id != device.Id) && ((Device)parentDevice).TypeId == parentType.Id)).ToList();
            //devicesDic.Values.ForEach(childDevice =>
            //    {
            //        if ((((Device)childDevice).TypeId == device.TypeId) && (childDevice.ParentId != 0) && (parentListByParentTypeId.Contains(devicesDic[childDevice.ParentId])))
            //            parentListByParentTypeId.Remove(devicesDic[childDevice.ParentId]);
            //    });
            //return parentListByParentTypeId;
            return null;
        }

        private DataItem GetParentType(DataItem dataItem)
        {
            //if (dataItem.ParentId != 0)
            //{
            //    var parentType = dataItem.Factory.DataItemsDic[dataItem.ParentId];
            //    if (((DeviceType)parentType).IsMarker)
            //        return GetParentType(parentType);
            //    else
            //        return parentType;
            //}
            //else
                return null;
        }

        private DataItem GetWithoutParentItem(DataItemsFactory factory)
        {
            var emptyItem = factory.GetEmptyDataItem();
            emptyItem.Name = "Без родителя";
            return emptyItem;
        }

        private List<DataItem> GetParents(DataItem dataItem)
        {
            //var DataItemEnumerable = dataItem.Factory.DataItemsDic.Values.
            //    Where(item => item.Id != dataItem.Id).ToList();
            //if (dataItem.Id == 0)
            //    return DataItemEnumerable;
            //return FilterPossibleRecursion(DataItemEnumerable, dataItem.Id);
            return null;
        }
        
        private List<DataItem> FilterPossibleRecursion(List<DataItem> list, int id)
        {
            int listCount = list.Count;
            for (int i = 0; i < listCount; i++)
            {
                var item = list[i];
                if (item.ParentId == id)
                {
                    FilterPossibleRecursion(list, item.Id);
                    i = list.IndexOf(item)-1;
                    list.Remove(item);
                    listCount = list.Count;
                }
            }
            return list;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // fires when ItemsSource count changed
            if (IsEnabled)
            {
                var dataItem = (DataItem)DataContext; 
                var selectedItem = ((DataItem)this.SelectedItem);
                if ((dataItem != null) && (selectedItem !=null))
                    dataItem.ParentId = selectedItem.Id;
            }
        }

        public void Update(DataItemControllerChangedEventArgs Change)
        {
            try
            {
                //if ((Change.Action == NotifyCollectionChangedAction.Replace) && (Change.NewDataItem.Id != ((DataItem)DataContext).Id))
                //    return;
                //this.RefreshDataContext(DataContext);
            }
            catch (Exception)
            { }
        }


    }
}
