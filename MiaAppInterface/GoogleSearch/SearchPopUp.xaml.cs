using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Controls.Primitives;

namespace MiaAppInterface
{
    /// <summary>
    /// Логика взаимодействия для SearchPopUp.xaml
    /// </summary>
    public partial class SearchPopUp : Popup
    {
        public SearchPopUp()
        {
            InitializeComponent();
            
        }

        private void PopUpGrid_LostMouseCapture(object sender, MouseEventArgs e)
        {
            Mouse.Capture(PopUpGrid, CaptureMode.SubTree);
        }

        private void Popup_Opened(object sender, EventArgs e)
        {
            Mouse.Capture(PopUpGrid, CaptureMode.SubTree);
        }

        private void PopUpGrid_PreviewMouseDownOutsideCapturedElement(object sender, MouseButtonEventArgs e)
        {
            IsOpen = false;
        }
    }
}
