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

namespace MiaAppInterface
{
    /// <summary>
    /// Логика взаимодействия для DataItemsGrid.xaml
    /// </summary>
    public abstract partial class DataItemsGrid : Grid
    {
        public bool ChangeMode { get; set; }
        public void Refresh(object DataContext)
        {
                this.RefreshDataContext(DataContext);
                RefreshComboBoxes(DataContext);
        }

        public abstract void RefreshComboBoxes(object DataContext);
        public abstract void RefreshComboBoxes();
    }
}
