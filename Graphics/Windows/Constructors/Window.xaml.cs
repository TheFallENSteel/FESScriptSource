using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FESScript.Graphics.Windows.Constructors
{
    /// <summary>
    /// Interaction logic for Window.xaml
    /// </summary>
    public partial class Window : UserControl, INotifyPropertyChanged
    {
        public WindowPlacement WindowPlacement {  get; set; }

        public DataDisplay DataContent { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler WindowClosed;
        public static readonly RoutedEvent HighlightEvent = EventManager.RegisterRoutedEvent("Highlight", RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(Window));
        public event RoutedEventHandler Highlight 
        {
            add 
            { 
                this.AddHandler(HighlightEvent, value);
            }
            remove 
            {
                this.RemoveHandler(HighlightEvent, value);
            }
        }

        public void DoHighlight() 
        {
            int zIndex = Canvas.GetZIndex(this);
            Canvas.SetZIndex(this, 100);
            RaiseEvent(new RoutedEventArgs(HighlightEvent));
            HighlightStoryboard.Completed += (e, args) => Canvas.SetZIndex(this, zIndex);
        }

        public Window(WindowPlacement windowPlacement, DataDisplay dataDisplay)
        {
            this.WindowPlacement = windowPlacement;
            this.DataContent = dataDisplay;
            InitializeComponent();
        }


        private void mainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete) 
            {
                WindowClosed?.Invoke(this, EventArgs.Empty);
                Delete();
            }
        }
        public void Delete() 
        { 
            (this.Parent as Canvas).Children.Remove(this);
            WindowPlacement.Remove();
            DataContent = null;
        }
    }
}
