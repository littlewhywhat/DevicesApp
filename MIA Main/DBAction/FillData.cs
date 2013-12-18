using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace MiaMain
{
    public abstract class FillData : DBAction
    {
        protected DataItemsFactory Factory { get; set; }
        public FillData(DataItemsFactory factory)
        {
            Factory = factory;
        }
        public void Act(SqlConnection connection)
        {
            using (var reader = DBHelper.GetCommand(GetCommandText(), connection).ExecuteReader())
            {
                while (reader.Read())
                {
                    Fill(reader, GetDataItem());                  
                }
            }
        }
        protected abstract DataItem GetDataItem();
        protected abstract string GetCommandText();
        protected virtual void Fill(SqlDataReader reader, DataItem dataItem)
        {
            Enumerable.Range(0, reader.FieldCount).ForEach(index =>
            {
                var fieldName = reader.GetName(index);
                var deviceProperty = dataItem.GetType().GetProperty(fieldName);
                deviceProperty.SetValue(dataItem, Convert.ChangeType(reader[fieldName], deviceProperty.PropertyType));
            });
        }
    }
}
