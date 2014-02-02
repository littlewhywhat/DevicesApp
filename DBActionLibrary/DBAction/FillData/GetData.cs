using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Data.Common;
using DataItemsLibrary;

namespace DBActionLibrary
{
    internal abstract class GetData : DBAction
    {
        public object Act(DbConnection connection)
        {
            using (var reader = DBHelper.GetCommand(GetCommandText(), connection).ExecuteReader())
            {
                while (reader.Read())
                {
                    Fill(reader, GetDataItem());                  
                }
            }
            return null;
        }
        protected virtual void Fill(DbDataReader reader, DataItem dataItem)
        {
            Enumerable.Range(0, reader.FieldCount).ToList().ForEach(index =>
            {
                var fieldName = reader.GetName(index);
                var deviceProperty = dataItem.GetType().GetProperty(fieldName);
                deviceProperty.SetValue(dataItem, Convert.ChangeType(reader[fieldName], deviceProperty.PropertyType),null);
            });
        }
        protected abstract DataItem GetDataItem();
        protected abstract string GetCommandText();
    }
}
