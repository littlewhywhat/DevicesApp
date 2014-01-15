using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using MiaMain;

namespace MiaAppInterface
{
    public abstract class DataItemsGrid : Grid
    {
        public DataItemsController Controller {get; private set;}
        public DataItemsGrid(DataItemsController controller)
        {
            Controller = controller;
        }
        public void UpdateDataItem()
        {
            var dataItem = DataContext as DataItem;
            Controller.UpdateDataItem(dataItem, this);
        }
    }
}
