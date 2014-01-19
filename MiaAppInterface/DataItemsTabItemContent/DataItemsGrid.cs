using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using MiaMain;

namespace MiaAppInterface
{
    public abstract class DataItemsGrid : Grid
    {
        public bool ChangeMode { get; set; }
        public void UpdateDataItem()
        {
            var dataItem = DataContext as DataItem;
            if (dataItem.Id == 0)
                dataItem.Insert();
            else
                dataItem.Update();
        }

    }
}
