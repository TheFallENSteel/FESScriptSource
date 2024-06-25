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
    /// Interaction logic for CustomtitleBar.xaml
    /// </summary>
    public partial class CustomtitleBar : UserControl
    {
        /// <summary>
        /// True shuts down app on click. Default is false.
        /// </summary>
        public bool ShutDownApp { set; get; }

        /// <summary>
        /// Gets or sets Title of Window.
        /// </summary>
        
        public string TitleText
        {
            set => saveText.Text = value;
            get => saveText.Text;
        }

        public CustomtitleBar()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Button to close the application.
        /// </summary>

        private void CloseButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            CurrentWindow.Close();
            if(ShutDownApp) 
            {
                Application.Current.Shutdown(0);
                Environment.Exit(0);
            }
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
