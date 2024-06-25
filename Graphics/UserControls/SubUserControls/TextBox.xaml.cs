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
    public partial class TextBox : UserControlPlus, IContents
    {

        public bool IsCompiler { get; set; }
        public bool QuotationMarks { get => true; }
        public int ID { get; set; }
        public new string Name { get => $"B{ID}"; }
        public string Text { get; set; }

        public TextBox()
        {
            InitializeComponent();
            DataContext = this;
        }

    }
}
