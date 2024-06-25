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
    /// Interaction logic for CustomLeftBar.xaml
    /// </summary>
    public partial class CustomLeftBar : UserControl
    {
        public CustomLeftBar()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Allows user to move with window while click is pressed.
        /// </summary>

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            CurrentWindow.DragMove();
        }

        private Window CurrentWindow 
        {
            get => ((Window)((Grid)this.Parent).Parent);
        }
    }
}
