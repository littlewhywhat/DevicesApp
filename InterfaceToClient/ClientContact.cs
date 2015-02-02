using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace InterfaceToClient
{
    public static class ClientContact
    {
        const string _deleteObject = "Delete the element";
        const string _confirmMessage = "Do you really want to run this operation?";
        public static bool Confirm()
        {
            return MessageBox.Show(_confirmMessage, _deleteObject, MessageBoxButton.YesNo) == MessageBoxResult.Yes;
        }
    }
}
