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
using InterfaceToDataBase;

namespace InterfaceToClient
{
    /// <summary>
    /// Логика взаимодействия для ListBoxGrid.xaml
    /// </summary>
    public partial class ListBoxItemGrid : ControlManager
    {
        
        public ListBoxItemGrid()
        {
            
            InitializeComponent();
        }

        public override IClose Closer { get { return (IClose)this.FindParentByType<IClose>(); } set { } }

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

        private void Undo_Click(object sender, RoutedEventArgs e) { Closer.Close(this); }

        protected override void EnableInsertMode()
        {
            base.EnableInsertMode();
            HideFrameworkElement(true, CancelButton);
            HideFrameworkElement(false, Undo);
        }

        public override void SwitchChangeMode(bool position)
        {
            base.SwitchChangeMode(position);
            HideFrameworkElement(false, CancelButton);
            HideFrameworkElement(true, Undo);
        }

        private void ListBoxItemGrid_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext != null)
            {
                if (CurrentDataItemController.InsertMode)
                    EnableInsertMode();
            }
        }

    }
}
