using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataItemsLibrary;

namespace InterfaceToClient
{
    public class CompanyIdComboBox : DeviceExtraComboBox
    {
        public CompanyIdComboBox() : base(TableNames.Companies)
        { }

    }
}
