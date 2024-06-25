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
    public class Combobox : ComboBox, IContents
    {
        public Combobox() :base()
        {
            MinWidth = 25;
            Height = 15;
            Margin = new Thickness(7.5, 5, 7.5, 5);
            VerticalAlignment = VerticalAlignment.Center;
            FontSize = 12.5;
            FontFamily = new FontFamily("Times New Roman");
            BorderThickness = new Thickness(0);
        }

        public bool IsCompiler{ get; set; }

        public bool QuotationMarks { get => true; }

        public int ID{ get; set; }

        public new string Name { get => $"M{ID}"; }

        public new string Text
        {
            get
            {
                return (string)SelectedItem;
            }
            set
            {
                SelectedItem = value;
            }
        }
    }
}
