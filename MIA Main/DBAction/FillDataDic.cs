using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace MiaMain
{
    public class FillDataDic : FillData
    {
        public FillDataDic(DataItemsFactory factory) : base(factory)
        { }
        protected override void Fill(SqlDataReader reader, DataItem dataItem)
        {
            base.Fill(reader, dataItem);
            Factory.GetDataItemsDic().Add(dataItem.Id, dataItem);
        }
        protected override string GetCommandText()
        {
            return DBHelper.GetFillCommandText(Factory.FirstTableFields, Factory.TableName);
        }

        protected override DataItem GetDataItem()
        {
            return Factory.GetDataItem();
        }
    }
}
