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

namespace FESScript2.Graphics.SupportWindow
{
    /// <summary>
    /// Interaction logic for CommandLine.xaml
    /// </summary>
    public partial class CommandLine : UserControl
    {
        public CommandLine()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Sets the text of command line.
        /// </summary>

        public string Text 
        { 
            set 
            {
                commandLineText.Text = value;
            }
            get 
            {
                return commandLineText.Text;
            }
        }

        /// <summary>
        /// Sends data from App Console to system Console.
        /// </summary>

        private void commandLineText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) 
            {
                ((Console.Console)((Grid)Parent).Parent).Write(Text);
                Text = "";
            }
        }
    }
}
