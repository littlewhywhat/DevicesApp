using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Threading;
using System.Collections;

namespace MiaMain
{
    public static class Extensions
    {
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach(T item in enumerable)
            {
                action(item);
            }
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
                    if ((dataItemProperty.Name != "Id") && (dataItemProperty.Name != "Factory"))
                        dictionary.Add(dataItemProperty.Name, dataItemProperty.GetValue(dataItem).ToString());
                });
            return dictionary;
        }

        public static Dictionary<string, string> GetSearchPropertyValueDic(this DataItem dataItem)
        {
            var dictionary = new Dictionary<string, string>();
            dataItem.GetType().GetProperties().ForEach(dataItemProperty =>
            {
                if ((dataItem.Factory.FirstTableFields.Contains(dataItemProperty.Name)))
                    dictionary.Add(dataItemProperty.Name, dataItemProperty.GetValue(dataItem).ToString());
            });
            return dictionary;
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
            var dataItemClone = dataItem.Factory.GetDataItem();
            dataItemClone.Id = dataItem.Id;
            dataItem.GetPropertyValueDic().ForEach(propertyValue => 
                dataItemClone.GetType().GetProperty(propertyValue.Key).SetValue(dataItemClone, propertyValue.Value));
            return dataItemClone;
        }
    }

}
