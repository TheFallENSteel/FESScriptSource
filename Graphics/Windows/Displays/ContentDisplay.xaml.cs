using FESScript.CodeWorks.BlockCreation.Blocks.Templates;
using FESScript.CodeWorks.BlockCreation.Blocks.UserElements.Contents.Args;
using FESScript.Graphics.Windows.Constructors;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
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

namespace FESScript.Graphics.Windows.Displays
{
    /// <summary>
    /// Interaction logic for DotDisplay.xaml
    /// </summary>
    public partial class ContentDisplay : DataDisplay
    {
        public ContentTemplate ContentTemplate { get; set; }

        public ContentDisplay(ContentTemplate template)
        {
            InitializeComponent();
            this.ContentTemplate = template;
            this.DataContext = template;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public override object GetData()
        {
            return this.ContentTemplate;
        }

        public override void SetData(object data)
        {
            Debug.Assert(data is ContentTemplate);
            ContentTemplate = data as ContentTemplate;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            (ContentTemplate.Parent as BlockTemplate).Update();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ContentTemplate.InitialValue is StringArgs stringArgs)
            {
                stringArgs.Value = (sender as TextBox)?.Text;
                ContentTemplate.InitialValue = stringArgs;
                (ContentTemplate.Parent as BlockTemplate).Update();
            }
        }
    }
}

/*
    [XmlAttribute] public int ID { get; set; } = iD;

    [XmlAttribute] public object Value = value;

    [XmlAttribute] public string Name { get; set; } = name;
    [XmlAttribute] public string Description { get; set; } = description;

    [XmlAttribute] public System.Type Type { get; set; } = type;
 */
