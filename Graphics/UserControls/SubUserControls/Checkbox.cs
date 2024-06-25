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
    public class Checkbox : CheckBox, IContents
    {
        public bool IsCompiler { get; set; }
        public bool QuotationMarks { get => false; }
        public int ID { get; set; }
        public new string Name { get => $"C{ID}"; }
        public string Text
        {
            get => IsChecked.Value ? "true" : "false";
            set { if (value == "true") IsChecked = true; else IsChecked = false; }
        }

        public Checkbox() : base()
        {
            
        }
    }
}
