using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterfaceToClient
{
    public class ParentComboBox : DataItemsComboBox
    {
        public ParentComboBox() : base()
        { }

        protected override DataItemController GetCurrentSelection()
        {
            return CurrentDataContext.Parent;
        }

        protected override IEnumerable<DataItemController> GetControllers()
        {
            return CurrentDataContext.GetPossibleParents();
        }

        protected override void ChangeCurrentDataContextBySelectedItem()
        {
            CurrentDataContext.Parent = CurrentSelectedItem;
        }
    }
}
