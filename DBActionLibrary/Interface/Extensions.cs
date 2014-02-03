using System;
using System.Collections.Generic;
using System.Threading;
using System.Collections;
using System.Linq;
using DataItemsLibrary;

namespace DBActionLibrary
{
    public static class Extensions
    {

        internal static Dictionary<string, string> GetPropertyValueDic(this DataItem dataItem)
        {
            var dictionary = new Dictionary<string, string>();
            dataItem.GetType().GetProperties().ToList().ForEach(dataItemProperty =>
                {
                    if ((dataItemProperty.Name != "Id") && (dataItemProperty.Name != "Factory"))
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

        public static DataItem GetFirstFilledDataItem(this DataItemsFactory factory, int Id)
        {
            var dataItem = factory.GetEmptyDataItem();
            dataItem.Id = Id;
            dataItem.Fill(factory.FirstTableFields);
            return dataItem;
        }

        public static DataItem Clone(this DataItem dataItem)
        {
            var dataItemClone = dataItem.Factory.GetEmptyDataItem();
            dataItem.GetType().GetProperties().ToList().ForEach(propertyInfo =>
            {
                if ((propertyInfo.Name != "Factory") && (propertyInfo.Name != "Marker"))
                    dataItemClone.GetType().GetProperty(propertyInfo.Name).SetValue(dataItemClone, Convert.ChangeType(propertyInfo.GetValue(dataItem, null), propertyInfo.PropertyType), null);
            });
            return dataItemClone;
        }
        public static void Fill(this IDataItemDic dataItemsDic, DataItemsFactory factory)
        {
            DBHelper.PerformDBAction(new FillDataDic(factory, dataItemsDic));
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
            DBHelper.PerformDBAction(new UpdateDataItem(dataItem));
        }

        public static void PerformActions(this List<DataItemAction> ActionsList)
        {
            DBHelper.PerformDBAction(new PerformTransactionOnList(TransactionDataConverter.Convert(ActionsList)));
        }

        public static void DeleteWithReferences(this DataItem dataItem, List<DataItemAction> ReferencesList)
        {
            var transactionsList = TransactionDataConverter.Convert(ReferencesList);
            transactionsList.Add(new DeleteDataItem(dataItem));
            DBHelper.PerformDBAction(new PerformTransactionOnList(transactionsList));
        }

        public static void Delete(this DataItem dataItem)
        {
            DBHelper.PerformDBAction(new DeleteDataItem(dataItem));         
        }
        public static void Insert(this DataItem dataItem)
        {
            try
            {
                DBHelper.PerformDBAction(new InsertDataItem(dataItem));
            }
            catch (DBActionException exception) { dataItem.Id = 0; throw exception; }
        }
        public static void Fill(this DataItem dataItem, List<string> tableFields)
        {
            DBHelper.PerformDBAction(new FillDataItem(dataItem, tableFields));
        }


    }

}
