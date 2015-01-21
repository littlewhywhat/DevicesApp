using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;

namespace CustomElements
{
    /// <summary>
    /// Логика взаимодействия для TestWindow.xaml
    /// </summary>
    public partial class TestWindow : Window
    {
        ComboBoxContext comboBoxContext = new ComboBoxContext();
        public TestWindow()
        {
            InitializeComponent();
            Main.DataContext = comboBoxContext;
            comboBoxContext.Collection = new List<string>() { "dfsdf", "sdfsdf" };
            
        }

        class ComboBoxContext : INotifyPropertyChanged
        {
            protected void OnPropertyChanged(string propertyName)
            {
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
            public ComboBoxContext()
            { }
            private IEnumerable<string> collection;
            public IEnumerable<string> Collection { get { return collection; } set { collection = value; OnPropertyChanged("Collection"); } }
            public event PropertyChangedEventHandler PropertyChanged;
        }

        private void ChangeCollection_Click(object sender, RoutedEventArgs e)
        {
            ChangeCollection();
        }

        private void ChangeCollection()
        {
            comboBoxContext.Collection = new List<string>() { "dfssddf", "sdfssddf" };
        }
    }
}
