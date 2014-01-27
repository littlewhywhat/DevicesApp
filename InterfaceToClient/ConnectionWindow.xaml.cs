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
using System.Data.Common;
using InterfaceToDataBase;
namespace InterfaceToClient
{
    /// <summary>
    /// Логика взаимодействия для ConnectionWindow.xaml
    /// </summary>
    public partial class ConnectionWindow : Window
    {
        public ConnectionWindow()
        {
            InitializeComponent();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            CloseDialogWindow(false);
        }

        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            Connection.ConnectionString = ((DbConnectionStringBuilder)DataContext).ConnectionString;
            if (Connection.CheckConnection())
            {
                CloseDialogWindow(true);
                ConnectionStringBuilder.SaveConnectionStringToFile(Connection.ConnectionString);
            }
            else
                MessageBox.Show("Oшибка подключения", "Oшибка", MessageBoxButton.OK);
        }
        private void CloseDialogWindow(bool result)
        {
            DialogResult = result;
            Close();
        }
    }
}
