using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace InterfaceToClient
{
    public class DataContextControl<T> : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        public T Element { get; private set; }
        public void SetElement(INotifyPropertyChanged dataElement)
        {
            Element = (T)dataElement;
            dataElement.PropertyChanged += dataElement_PropertyChanged;
            OnPropertyChanged();
        }
        public void Refresh()
        {
            OnPropertyChanged();
        }

        private void dataElement_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged();
        }
        const string _Element = "Element";
        protected void OnPropertyChanged()
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(_Element));
        }

    }
}
