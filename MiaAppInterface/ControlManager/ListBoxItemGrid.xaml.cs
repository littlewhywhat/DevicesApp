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
using MiaMain;

namespace MiaAppInterface
{
    /// <summary>
    /// Логика взаимодействия для ListBoxGrid.xaml
    /// </summary>
    public partial class ListBoxItemGrid : ControlManager
    {
        private IClose closer;
        public ListBoxItemGrid()
        {
            InitializeComponent();
        }

        public override IClose Closer { get { return closer; } set { closer = value; } }

        protected override Grid SocketGrid { get { return socketGrid; } }

        protected override DataItemsChange CurrentChange { get { return (DataItemsChange)DataContext; } }

        protected override Button UpdateButton { get { return Update; } }

        protected override Button DeleteButton { get { return Delete; } }

        protected override Button ChangeButton { get { return Change; } }

        protected override Button CancelButton { get { return Cancel; } }

        private void Update_Click(object sender, RoutedEventArgs e) { UpdateClick(); }

        private void Delete_Click(object sender, RoutedEventArgs e) { DeleteClick(e); }

        private void Change_Click(object sender, RoutedEventArgs e) { ChangeClick(); }

        private void Cancel_Click(object sender, RoutedEventArgs e) { CancelClick(); }

        private void Undo_Click(object sender, RoutedEventArgs e) { Closer.Close(this); }

        public override void EnableInsertMode()
        {
            base.EnableInsertMode();
            Cancel.Visibility = System.Windows.Visibility.Hidden;
            Undo.Visibility = System.Windows.Visibility.Visible;
        }

        public override void SwitchChangeMode(bool position)
        {
            base.SwitchChangeMode(position);
            Cancel.Visibility = System.Windows.Visibility.Visible;
            Undo.Visibility = System.Windows.Visibility.Hidden;
        }

        private void ListBoxItemGrid_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext != null)
                PossessChangeOfCurrentChange();
        }
    }
}
