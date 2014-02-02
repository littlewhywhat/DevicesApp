using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataItemsLibrary;

namespace DBActionLibrary
{
    internal static class TransactionDataConverter
    {
        public static TransactionData Convert(DataItemAction dataItemAction)
        {
            switch (dataItemAction.Action)
            {
                case (ActionType.DELETE) :
                    return new DeleteDataItem(dataItemAction.DataItem);
                case (ActionType.UPDATE) :
                    return new UpdateDataItem(dataItemAction.DataItem);
                case (ActionType.INSERT) :
                    return new InsertDataItem(dataItemAction.DataItem);
            }
            return null;
        }

        public static List<TransactionData> Convert(List<DataItemAction> dataItemActionList)
        {
            return dataItemActionList.Select(dataItemAction => (TransactionData)TransactionDataConverter.Convert(dataItemAction)).ToList();
        }
    }
}
