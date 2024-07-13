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

namespace FESScript.Settings.Support
{
    /// <summary>
    /// Interaction logic for CustomtitleBar.xaml
    /// </summary>
    public partial class CustomTitleBar : UserControl
    {
        /// <summary>
        /// True shuts down app on click. Default is false.
        /// </summary>
        public bool ShutDownApp { set; get; }

        public MainWindow MainWindow 
        { 
            get => App.Window;
        }

        /// <summary>
        /// Gets or sets Title of Window.
        /// </summary>
        
        public string TitleText 
        { 
            get => GetValue(TitleTextProperty).ToString();
            set => SetValue(TitleTextProperty, value); 
        }

        public static readonly DependencyProperty TitleTextProperty = DependencyProperty.Register(
            "TitleText", 
            typeof(string), 
            typeof(CustomTitleBar), 
            new PropertyMetadata("FESProject")
            );
        public CustomTitleBar()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        /// <summary>
        /// Button to close the application.
        /// </summary>

        private void CloseButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.MainWindow.Close();
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
            this.MainWindow.DragMove();
        }
    }
}
