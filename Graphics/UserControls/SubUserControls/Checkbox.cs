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
        public Checkbox() : base()
        {
            
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

        const bool quotationMarks = false;

        public string Text 
        { 
            get 
            { 
                if ((bool)IsChecked) 
                {
                    return "true";
                }
                else 
                {
                    return "false";
                }
            }
            set 
            {
                if (value == "true")
                {
                    IsChecked = true;
                }
                else if (value == "false")
                {
                    IsChecked = false;
                }
            }
        }
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
                return $"C{Id}";
            }
        }

    }
}
