using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.ComponentModel;
using FESScript.CodeWorks.Functions;
using FESScript.CodeWorks.BlockCreation.Blocks.Templates;
using FESScript.CodeWorks.BlockCreation.Blocks.Placements;
using FESScript.CodeWorks.BlockCreation.Blocks.UserElements.Contents;
using CheckBox = FESScript.CodeWorks.BlockCreation.Blocks.UserElements.Contents.CheckBox;
using TextBox = FESScript.CodeWorks.BlockCreation.Blocks.UserElements.Contents.TextBox;
using FESScript.Graphics.Windows.Displays;
using FESScript.CodeWorks.BlockCreation.Blocks.UserElements.Contents.Args;
using FESScript.CodeWorks.CodeExecution;
using FESScript.Graphics.UserControls;
using FESScript.Prototyping;
using UserControlManager = FESScript.CodeWorks.UserControls.UserControlManager;

namespace FESScript
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public const double Version = 3.0;
        public Settings Settings { get; private set; } = new Settings();
        public UserControlManager UserControlManager { get; private set; }
        public CodeExecutionManager CodeExecutionManager { get; private set; } = new CodeExecutionManager();
        public BlockTemplateManager BlockTemplateManager { get; private set; } = new BlockTemplateManager();
        public Graphics.UserControls.SubUserControls.Menu MainMenu => this.menu;
        public Canvas MainCanvas => this.mainCanvas;

        Prototype prototype;
        public MainWindow()
        {
            Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
            Application.Current.MainWindow = this;
            InitializeComponent();
            UserControlManager = new UserControlManager(this, mainCanvas);
            prototype = new Prototype(this);
            prototype.OnWindowConstruct();
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            UserControlManager.OnKeyDown(e.Key);
        }

        private void Window_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            UserControlManager.OnMouseWheel(sender, e);
        }
        public void UpdateGlobalPosition() 
        {
            UserControlManager.UpdateGlobalPosition();
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            UserControlManager.OnMouseDown(e);
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            UserControlManager.OnMouseMove(e);
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            UserControlManager.OnKeyUp(e.Key);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CodeExecutionManager.CompileCode(null);
        }
    }
}
