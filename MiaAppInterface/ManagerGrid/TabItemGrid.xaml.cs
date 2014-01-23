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
using MiaMain;

namespace MiaAppInterface
{
    /// <summary>
    /// Логика взаимодействия для TabItemGrid.xaml
    /// </summary>
    public partial class TabItemGrid : ManagerGrid
    {
        public TabItemGrid()
        {
            InitializeComponent();
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public override DataItemsInfoGrid ContentGrid { get { return (DataItemsInfoGrid)contentGrid.Children[0]; } set { contentGrid.Children.Add(value); } }
        
        protected override IClose Closer { get { return (IClose)Parent; } }

        protected override DataItemsChange CurrentChange { get { return (DataItemsChange)DataContext; } }

        protected override Button UpdateButton { get { return Update; } }

        protected override Button DeleteButton { get { return Delete; } }

        protected override Button ChangeButton { get { return Change; } }

        protected override Button CancelButton { get { return Cancel; } }

        private void Update_Click(object sender, RoutedEventArgs e) { UpdateClick(); }

        private void Delete_Click(object sender, RoutedEventArgs e) { DeleteClick(e); }

        private void Change_Click(object sender, RoutedEventArgs e) { ChangeClick(); }

        private void Cancel_Click(object sender, RoutedEventArgs e) { CancelClick(); }

        private void TabItemGrid_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext != null)
                PossessChangeOfCurrentChange();
        }
    }
}
