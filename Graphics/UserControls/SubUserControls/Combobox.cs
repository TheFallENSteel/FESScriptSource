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

        private bool isCompiler;

        public bool IsCompiler
        {
            get
            {
                return isCompiler;
            }
            set
            {
                isCompiler = value;
            }
        }

        public bool QuotationMarks
        {
            get
            {
                return quotationMarks;
            }
        }

        const bool quotationMarks = true;

        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }
        private int id;

        public new string Name
        {
            get
            {
                return $"M{Id}";
            }
        }

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
