using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace MiaMain
{
    public class FillDataDic : DBAction
    {
        private DataItemsFactory Factory { get; set; }
        public FillDataDic(DataItemsFactory factory)
        {
            Factory = factory;
        }

        public void Act(SqlConnection connection)
        {
            using (var reader = DBHelper.GetCommand(DBHelper.GetFillCommandText(Factory.FirstTableFields, Factory.TableName), connection).ExecuteReader())
            {
                while (reader.Read())
                {
                    var dataItem = Factory.GetDataItem();
                    DBHelper.FillDataItem(reader, dataItem);
                    Factory.GetDataItemsDic().Add(dataItem.Id, dataItem);   
                }
            }
        }
    }
}
