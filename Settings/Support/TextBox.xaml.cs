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

namespace FESScript2.Settings.Support
{
    /// <summary>
    /// Interaction logic for TextBox.xaml
    /// </summary>
    public partial class TextBox : UserControl
    {
        public event TextChangedEventHandler OnTextChanged;
        public TextBox()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets the main text.
        /// </summary>

        public string Text 
        { 
            get 
            {
                return PathBox.Text;
            }
            set
            {
                PathBox.Text = value;
            }
        }

        private void PathBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            OnTextChanged?.Invoke(this, null);
        }
    }
}
