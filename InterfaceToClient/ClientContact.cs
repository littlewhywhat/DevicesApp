using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace InterfaceToClient
{
    public static class ClientContact
    {
        const string _deleteObject = "Удаление элемента";
        const string _confirmMessage = "Вы действительно хотить выполнить данную операцию?";
        public static bool Confirm()
        {
            return MessageBox.Show(_confirmMessage, _deleteObject, MessageBoxButton.YesNo) == MessageBoxResult.Yes;
        }
    }
}
