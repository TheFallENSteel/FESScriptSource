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

        /// <summary>
        /// Creates textbox for blocks.
        /// </summary>
        /// <param name="OnlyNumbers">Specifies if user is able to write just numbers.</param>

        public TextBox()
        {
            InitializeComponent();
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
                return $"B{Id}";
            }
        }

        public string Text 
        {
            get 
            {
                return textBox.Text;
            }
            set
            {
                textBox.Text = value;
            }
        }
    }
}
