using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace InterfaceToDataBase
{
    
    [Serializable()]
    public class DBActionException : System.Exception
    {
        const string _exceptionMessage = "Ошибка при выполнении транзакции. Попробуйте снова.";
        public DBActionException() : base() { }
        public DBActionException(string message) : base(message) { }
        public DBActionException(string message, System.Exception inner) : base(message, inner) { }
        public DBActionException(System.Exception inner) : base(_exceptionMessage, inner) { }
        public void ShowMessageBox()
        {
            MessageBox.Show("Oшибка", _exceptionMessage , MessageBoxButton.OK);
        }
        // A constructor is needed for serialization when an
        // exception propagates from a remoting server to the client. 
        protected DBActionException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) { }
    }
}
