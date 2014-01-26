using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MiaMain
{
    public abstract class DataItem
    {
        public DataItemsFactory Factory { get; private set; }
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string Name { get; set; }
        public DataItem(DataItemsFactory factory)
        {
            Factory = factory;
        }
        public bool Equals(DataItem dataItem)
        {
            return (IsTheSameType(dataItem) && Id == dataItem.Id);
        }
        public abstract bool IsTheSameType(DataItem dataItem);

        public virtual Dictionary<string, string> GetSearchPropertyValueDic()
        {
            var dictionary = new Dictionary<string, string>();
            GetType().GetProperties().ForEach(dataItemProperty =>
            {
                if ((Factory.SearchTableFields.Contains(dataItemProperty.Name)))
                    dictionary.Add(dataItemProperty.Name, dataItemProperty.GetValue(this, null).ToString());
            });
            return dictionary;
        }
    }
}
