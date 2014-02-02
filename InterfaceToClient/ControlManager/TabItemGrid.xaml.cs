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
using DataItemsLibrary;

namespace InterfaceToClient
{
    /// <summary>
    /// Логика взаимодействия для TabItemGrid.xaml
    /// </summary>
    public partial class TabItemGrid : ControlManager
    {
        public TabItemGrid()
        {
            InitializeComponent();
        }

        private IClose GetCloser(FrameworkElement element)
        {
            var parent = element.Parent as FrameworkElement;
            if ((parent == null) || (parent is IClose))
                return parent as IClose;
            else
                return GetCloser(parent);
        }

        public override IClose Closer { get { return GetCloser(this); } set { } }

        protected override Grid SocketGrid { get { return socketGrid; } }

        public override DataItemController CurrentDataItemController { get { return (DataItemController)DataContext; } }

        protected override FrameworkElement UpdateButton { get { return Update; } }

        protected override FrameworkElement DeleteButton { get { return Delete; } }

        protected override FrameworkElement ChangeButton { get { return Change; } }

        protected override FrameworkElement CancelButton { get { return Cancel; } }

        private void Update_Click(object sender, RoutedEventArgs e) { UpdateClick(); }

        private void Delete_Click(object sender, RoutedEventArgs e) { DeleteClick(e); }

        private void Change_Click(object sender, RoutedEventArgs e) { ChangeClick(); }

        private void Cancel_Click(object sender, RoutedEventArgs e) { CancelClick(); }


        

        private void TabItemGrid_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext != null)
            {
                //if (CurrentDataItemController.InsertMode)
                //    EnableInsertMode();
                
            }
        }
    }
}
