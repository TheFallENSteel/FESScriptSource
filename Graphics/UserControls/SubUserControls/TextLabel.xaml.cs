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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FESScript2.Graphics.UserControls.SubUserControls
{
    /// <summary>
    /// Interaction logic for TextLabel.xaml
    /// </summary>
    public partial class TextLabel : UserControlPlus, IContents
    {
        public bool IsCompiler { get; set; }
        public int ID { get; set; }
        public bool QuotationMarks { get => true; }
        public new string Name { get => $"L{ID}"; }
        public string Text { get; set; }

        public TextLabel()
        {
            InitializeComponent();
            DataContext = this;
        }

    }
}
