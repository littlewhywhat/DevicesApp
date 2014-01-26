using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Threading;
using System.Collections;
using System.Linq;

namespace MiaMain
{
    public static class Extensions
    {
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            var list = new List<T>(enumerable);
            foreach(T item in list)
            {
                action(item);
            }
        }

        public static string ToString(this DateTime time)
        {
            return time.Year + "." + time.Month + "." + time.Day + " " + time.TimeOfDay.ToString();
        }

        public static void FillDic(this DataItemsFactory factory)
        {
            DBHelper.PerformDBAction(Connection.GetConnection(), new FillDataDic(factory));
        }

        public static Dictionary<string, string> GetPropertyValueDic(this DataItem dataItem)
        {
            var dictionary = new Dictionary<string, string>();
            dataItem.GetType().GetProperties().ForEach(dataItemProperty =>
                {
                    if ((dataItemProperty.Name != "Id") && (dataItemProperty.Name != "Factory") && (dataItemProperty.Name != "Marker"))
                    {
                        var value = dataItemProperty.GetValue(dataItem, null);
                        string result = null;
                        if (value != null)
                            if (value is DateTime)
                            {
                                var date = ((DateTime)value);
                                result = date.Year + "." + date.Month + "." + date.Day + " " + date.Hour + ":" + date.Minute + ":" + date.Second;
                            }
                            else
                                result = value.ToString();
                        dictionary.Add(dataItemProperty.Name, result);
                    }
                });
            return dictionary;
        }

        public static Dictionary<string, string> GetSearchPropertyValueDic(this DataItem dataItem)
        {
            var dictionary = new Dictionary<string, string>();
            dataItem.GetType().GetProperties().ForEach(dataItemProperty =>
            {
                if ((dataItem.Factory.SearchTableFields.Contains(dataItemProperty.Name)))
                    dictionary.Add(dataItemProperty.Name, dataItemProperty.GetValue(dataItem,null).ToString());
            });
            return dictionary;
        }

        public static void ChangeInDb(this DataItem dataItem)
        {
            if (dataItem.Id == 0)
                dataItem.Insert();
            else
                dataItem.Update();
        }
        public static void Update(this DataItem dataItem)
        {
            DBHelper.PerformDBAction(Connection.GetConnection(), new UpdateDataItem(dataItem));
        }
        public static void Delete(this DataItem dataItem)
        {
            DBHelper.PerformDBAction(Connection.GetConnection(), new DeleteDataItem(dataItem));         
        }
        public static void Insert(this DataItem dataItem)
        {
            DBHelper.PerformDBAction(Connection.GetConnection(), new InsertDataItem(dataItem));
        }
        public static void Fill(this DataItem dataItem, List<string> tableFields)
        {
            DBHelper.PerformDBAction(Connection.GetConnection(), new FillDataItem(dataItem, tableFields));
        }
        public static DataItem Clone(this DataItem dataItem)
        {
            var dataItemClone = dataItem.Factory.GetEmptyDataItem();
            dataItem.GetType().GetProperties().ForEach(propertyInfo =>
                {
                    if ((propertyInfo.Name != "Factory") && (propertyInfo.Name != "Marker"))
                        dataItemClone.GetType().GetProperty(propertyInfo.Name).SetValue(dataItemClone, Convert.ChangeType(propertyInfo.GetValue(dataItem, null), propertyInfo.PropertyType),null);
                });
            return dataItemClone;
        }

        public static string GetMarker(this DeviceType deviceType)
        {
            if (deviceType.ParentId != 0)
            {
                var parentType = (DeviceType)FactoriesVault.FactoriesDic[TableNames.DeviceTypes].GetDataItemsDic()[deviceType.ParentId];
                if (parentType.IsMarker)
                    return parentType.Name;
                else
                    return parentType.GetMarker();
            }
            return "Без маркера";
        }
    }

}
