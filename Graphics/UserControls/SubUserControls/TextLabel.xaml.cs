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
        public TextLabel()
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

        /// <summary>
        /// Text string.
        /// </summary>

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
                return $"L{Id}";
            }
        }

        public string Text
        {
            get
            {
                return text.Text;
            }
            set
            {
                text.Text = value;
            }
        }
    }
}
