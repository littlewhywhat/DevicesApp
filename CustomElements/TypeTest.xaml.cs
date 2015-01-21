using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml;
using System.Windows.Markup;
using System.ComponentModel;
using Xceed.Wpf.Toolkit;

namespace CustomElements
{
    /// <summary>
    /// Логика взаимодействия для TypeTest.xaml
    /// </summary>
    public partial class XamlReader : Window
    {
        public XamlReader()
        {
            
            InitializeComponent();
            System.Type controltype = typeof(Control);
            List<System.Type> derivedTypes = new List<System.Type>();

            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetAssembly(typeof(Control));
            foreach (System.Type type in assembly.GetTypes())
            {
                if (type.IsSubclassOf(controltype) && !type.IsAbstract && type.IsPublic)
                {
                    derivedTypes.Add(type);
                }
            }
            System.Reflection.Assembly assembly2 = System.Reflection.Assembly.GetAssembly(typeof(Xceed.Wpf.Toolkit.DateTimePicker));
            foreach (System.Type type in assembly2.GetTypes())
            {
                if (type.IsSubclassOf(controltype) && !type.IsAbstract && type.IsPublic)
                {
                    derivedTypes.Add(type);
                }
            }
            listbox.ItemsSource = derivedTypes;
            
            
            //Console.WriteLine(XamlWriter.Save(asd.Template));
            
        }

        private void listbox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            ListBox box = sender as ListBox;
            System.Type selected = (System.Type)box.SelectedValue;
            object item = Activator.CreateInstance(selected);
            ((Control)item).BeginInit();
            ((Control)item).EndInit();
            if (item is ComboBox)
            {
                var it = item as ComboBox;
                grid.Children.Add(it);
                it.IsEditable = true;
                it.Items.Add(new ComboBoxItem() { Content = new TextBlock() { Text = "asd" } });
                it.SelectedIndex = 0;
            }
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = new string(' ', 4);
            settings.NewLineOnAttributes = true;
            StringBuilder strbuild = new StringBuilder();
            XmlWriter xmlwrite = XmlWriter.Create(strbuild, settings);

            
            
            XamlWriter.Save(((Control)item).Template, xmlwrite);
            text.Text = strbuild.ToString();
        }
    }
}
