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
using FESScript.CodeWorks.Functions;
using FESScript.CodeWorks.BlockCreation.Blocks.Templates;
using FESScript.CodeWorks.BlockCreation.Blocks.Placements;
using FESScript.CodeWorks.BlockCreation.Blocks.UserElements.Contents;
using Block = FESScript.CodeWorks.BlockCreation.Blocks.UserElements.Block;
using CheckBox = FESScript.CodeWorks.BlockCreation.Blocks.UserElements.Contents.CheckBox;
using TextBox = FESScript.CodeWorks.BlockCreation.Blocks.UserElements.Contents.TextBox;
using FESScript.Graphics.UserControls.Connecting;
using FESScript.Settings;
using FESScript.Graphics.Windows.WindowData;
using FESScript.Graphics.Windows.Displays;
using FESScript.Graphics.Windows.Constructors;
using FESScript.CodeWorks.BlockCreation.Blocks.UserElements.Contents.Args;

namespace FESScript
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window, INotifyPropertyChanged
    {
        public const double Version = 3.0;
        const double CameraMovementMultiplier = 15;

        public event EventHandler CameraMoveEvent;
        public event PropertyChangedEventHandler PropertyChanged;

        public Block Start { get; set; }

        public Point CameraPosition {  get; set; }

        private string saveName;
        public string SaveName
        {
            get
            {
                return saveName;
            }
            set
            {
                saveName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(SaveName));
            }
        }

        public FESScript.CodeWorks.Console.Console MainConsole;
        public Graphics.UserControls.SubUserControls.Menu MainMenu => this.menu;
        public Canvas MainCanvas => this.mainCanvas;

        public static bool IsRunning { get; set; }

        private BlockTemplateLoader blockTemplateLoader = new BlockTemplateLoader(Directories.Blocks);

        private bool CanZoom {  get; set; }

        private bool IsFullScreen { get; set; }

        private bool MiddleMouseClicked { get; set; }

        public Point Offset { get; set; }

        private double zoom = 100;

        public double Zoom 
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
                Point mousePos = Mouse.GetPosition(mainCanvas);
                mainCanvas.RenderTransformOrigin = new Point(0.5, 0.5);
                mainCanvas.RenderTransform = new ScaleTransform(100 / zoom, 100 / zoom, 0, 0);

            }
        }

        /// <summary>
        /// Determines whether the window is running.
        /// </summary>

        //public static bool isRunning;

        public MainWindow()
        {
            Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
            Application.Current.MainWindow = this;
            blockTemplateLoader.Load();
            SaveName = "FESScriptFile";
            InitializeComponent();
            IsRunning = true;
            Closed += OnClosed;

            BlockTemplate blockTemplate = new BlockTemplate(
                    new System.Collections.ObjectModel.ObservableCollection<DotTemplate>()
                    {
                        new DotTemplate(Type.Numerical, IO.Output, 4),
                        new DotTemplate(Type.SubAction, IO.Output, 1),
                        new DotTemplate(Type.Action   , IO.Input, 2),
                        new DotTemplate(Type.Console  , IO.Input, 3),
                    },
                    new System.Collections.ObjectModel.ObservableCollection<ContentTemplate>()
                    {
                        new ContentTemplate(typeof(TextBox), 5, value:new StringArgs("Pche")),
                        new ContentTemplate(typeof(CheckBox), 6, value:new BoolArgs(true)),
                        new ContentTemplate(typeof(TextLabel), 7, value:new StringArgs("Ahojky")),
                        new ContentTemplate(typeof(Combobox), 8, value:new ComboBoxArgs(["Hi", "Hey", "Hate you!", "I Love you! I Hate you! I Love you! I Hate you! I Love you! I Hate you! I Love you! I Hate you! I Love you! I Hate you! I Love you! I Hate you! I Love you! I Hate you!"], -1)),
                    },
                    Type.SubAction);

            BlockPlacement x = new BlockPlacement(
                blockTemplate,
                null, 
                mainCanvas);
            pl = new Graphics.Windows.Constructors.WindowPlacement(null, mainCanvas, new Point(0,0), new BlockDisplay(blockTemplate));
            /*Graphics.Windows.Constructors.WindowPlacement windowPlacement =
                new Graphics.Windows.Constructors.WindowPlacement(
                    null,
                    mainCanvas,
                    new Point(10, 10),
                    new BlockTemplate() { }
                    );*/
        }
        public static Graphics.Windows.Constructors.WindowPlacement pl;
        private void OnClosed (object a, EventArgs e) 
        {
            IsRunning = false;
        }



        /// <summary>
        /// Handles key presses.
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// </summary>
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
                case Key.F11:
                    IsFullScreen = !IsFullScreen;
                    if (IsFullScreen) WindowState = WindowState.Maximized;
                    else WindowState = WindowState.Normal;
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

        /// <summary>
        /// Moves camera and Invokes <see cref="CameraMoveEvent"/>.
        /// </summary>
        /// <param name="x">Camera movement on X axis.</param>
        /// <param name="y">Camera movement on Y axis.</param>

        public void UpdateGlobalPosition() 
        {
            CameraMoveEvent?.Invoke(null, null);
        }

        private void CameraMove(double x, double y)
        {
            double posX, posY;
            CameraRelativePos(x, y, out posX, out posY);
            CameraPosition = new Point(posX, posY);
            CameraMoveEvent?.Invoke(null, EventArgs.Empty);
        }

        private void CameraRelativePos(double x, double y, out double posX, out double posY)
        {
            posX = CameraPosition.X + x;
            posY = CameraPosition.Y + y;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.MiddleButton == MouseButtonState.Pressed || e.RightButton == MouseButtonState.Pressed)
            {
                //Mouse.Capture(this);
                Offset = e.GetPosition(this);
                MiddleMouseClicked = true;
            }
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (MiddleMouseClicked && (e.MiddleButton == MouseButtonState.Pressed || e.RightButton == MouseButtonState.Pressed))
            {
                ChangePosition(e);
            }
            else if (MiddleMouseClicked)
            {
                MiddleMouseClicked = false;
                this.ReleaseMouseCapture();
            }
            if (!IMoveable.IsMovable && e.LeftButton == MouseButtonState.Pressed) 
            { 
                Connection.CurrentConnection.ContinueConnection(e.GetPosition(mainCanvas));
            }
            else if (!IMoveable.IsMovable)
            { 
                Connection.CurrentConnection.FinishConnection(null);
            }
        }

        private void ChangePosition(MouseEventArgs e)
        {
            double pointX = (-e.GetPosition((UIElement)this).X) + Offset.X;
            double pointY = (-e.GetPosition((UIElement)this).Y) + Offset.Y;
            this.CameraMove(pointX * (Zoom / 100), pointY * (Zoom / 100));
            Offset = e.GetPosition((UIElement)this);
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
