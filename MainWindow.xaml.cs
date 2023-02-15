using FESScript2.Graphics.UserControls;
using FESScript2.Graphics.UserControls.SubUserControls;
using System;
using System.Collections.Generic;
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
using System.Threading;
using System.ComponentModel;
using FESScript2.CodeWorks.Functions;

namespace FESScript2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //public delegate void CameraMoveEvent();
        public event EventHandler CameraMoveEvent;

        public Graphics.UserControls.Block Start;

        /// <summary>
        /// Position view by camera.
        /// </summary>

        public Point CameraPosition
        {
            get
            {
                return cameraPosition;
            }
            set
            {
                cameraPosition = value;
            }
        }

        public static string SaveName 
        { 
            get 
            {
                if (mainWindow != null)
                {
                    saveName = mainWindow.title.TitleText;
                }
                return saveName;
            }
            set 
            {
                saveName = value;
                if (mainWindow != null) 
                { 
                    mainWindow.title.TitleText = value;
                }
            }
        }

        private static string saveName;

        private Point cameraPosition;
        /// <summary>
        /// Main Console of the application.
        /// </summary>

        public static Console.Console MainConsole;

        /// <summary>
        /// Main Canvas of the application.
        /// </summary>

        public static MainWindow mainWindow;

        public static Graphics.UserControls.SubUserControls.Menu mainMenu;

        /// <summary>
        /// Determines whether the window is running.
        /// </summary>

        public static bool isRunning;

        public MainWindow()
        {
            Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
            Application.Current.MainWindow = this;
            CodeWorks.BlockCreation.Loader.Load();
            InitializeComponent();
            mainWindow = this;
            mainMenu = menu;
            isRunning = true;
            Closed += OnClosed;
        }

        private void OnClosed (object a, EventArgs e) 
        {
            isRunning = false;
        }

        static double zoom = 100;
        public static double Zoom 
        { 
            get 
            {
                return zoom;
            }
            set 
            { 
                if (value >= 199) 
                {
                    zoom = 199;
                } 
                else if (value <= 1)
                { 
                    zoom = 1;
                }
                else 
                {
                    zoom = value;
                }
                Point mousePos = Mouse.GetPosition(mainWindow.mainCanvas);
                MainWindow.mainWindow.mainCanvas.RenderTransformOrigin = new Point(0.5, 0.5);
                MainWindow.mainWindow.mainCanvas.RenderTransform = new ScaleTransform(100 / zoom, 100 / zoom, 0, 0);

            }
        }

        bool CanZoom;

        /// <summary>
        /// Handles key presses.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key) 
            {
                case Key.Left:
                    CameraMove(-1 * CameraMovementMultiplier, 0);
                    break;
                case Key.Right:
                    CameraMove(1 * CameraMovementMultiplier, 0);
                    break;
                case Key.Up:
                    CameraMove(0, -1 * CameraMovementMultiplier);
                    break;
                case Key.Down:
                    CameraMove(0, 1 * CameraMovementMultiplier);
                    break;
            }
            if (e.Key == Key.LeftCtrl || e.Key == Key.RightCtrl) 
            {
                CanZoom = true;
            }
        }

        private void Window_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (CanZoom && e.Delta != 0) 
            {
                Zoom -= e.Delta/50;
            }
        }
        const double CameraMovementMultiplier = 15;

        /// <summary>
        /// Moves camera and Invokes <see cref="CameraMoveEvent"/>.
        /// </summary>
        /// <param name="x">Camera movement on X axis.</param>
        /// <param name="y">Camera movement on Y axis.</param>

        public void UpdateGlobalPosition() 
        {
            mainWindow.CameraMoveEvent?.Invoke(null, null);
        }

        private void CameraMove(double x, double y) 
        {
            double posX = CameraPosition.X + x;
            double posY = CameraPosition.Y + y;
            CameraPosition = new Point(posX, posY);
            CameraMoveEvent?.Invoke(null, EventArgs.Empty);
        }
        bool MiddleMouseClicked;

        public static Point Offset;

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.MiddleButton == MouseButtonState.Pressed || e.RightButton == MouseButtonState.Pressed)
            {
                Mouse.Capture(this);
                Offset = e.GetPosition((UIElement)this);
                MiddleMouseClicked = true;
            }
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (MiddleMouseClicked && (e.MiddleButton == MouseButtonState.Pressed || e.RightButton == MouseButtonState.Pressed))
            {
                double pointX = (-e.GetPosition((UIElement)this).X) + Offset.X;
                double pointY = (-e.GetPosition((UIElement)this).Y) + Offset.Y;
                this.CameraMove(pointX * (Zoom / 100), pointY * (Zoom / 100));
                Offset = e.GetPosition((UIElement)this);
            }
            else if (MiddleMouseClicked)
            {
                MiddleMouseClicked = false;
                this.ReleaseMouseCapture();
            }
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftCtrl || e.Key == Key.RightCtrl)
            {
                CanZoom = false;
            }
        }
    }
}
