using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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
using FESScript.CodeWorks.BlockCreation.Blocks.Placements;
using FESScript.CodeWorks.BlockCreation.Blocks.Templates;
using FESScript.CodeWorks.BlockCreation.Blocks.UserElements;
using FESScript.CodeWorks.BlockCreation.Blocks.UserElements.Contents;
using FESScript.Graphics.UserControls.SubUserControls;
using FESScript.Graphics.Windows.Displays;
using Microsoft.Win32;


namespace FESScript.Graphics.UserControls.SubUserControls
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : UserControl
    {
        public SupportWindow.Expander Expander;
        public Menu()
        {
            InitializeComponent();
            Expander = expander;
        }

        private void ConsoleStart()
        {
            //App.Window.MainConsole = new FESScript.CodeWorks.Console.Console();
            //MainWindow.MainConsole.Show();

        }
        private void SaveClick(object sender, MouseButtonEventArgs e)
        {
            FileDialog fileDialog = new SaveFileDialog
            {
                Filter = "FESScript Project Files (*.fess)|*.fess|All files (*.*)|*.*",
            };
            fileDialog.ShowDialog();
            CodeWorks.Saving.Saver.SaveProject(BlockPlacement.blockPlacements, fileDialog.FileName, App.Window);
        }

        private void LoadClick(object sender, MouseButtonEventArgs e)
        {
            FileDialog fileDialog = new OpenFileDialog
            {
                Filter = "FESScript Project Files (*.fess)|*.fess|All files (*.*)|*.*",
            };
            fileDialog.ShowDialog();
            CodeWorks.Saving.Loader.LoadProject(fileDialog.FileName, App.Window, App.Window.BlockTemplateManager);
        }

        private void StartClick(object sender, MouseButtonEventArgs e)
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += async (s, args) =>
            {
                await (args.Argument as MainWindow).CodeExecutionManager.CompileCode(null); //TODO: Add StartBlock reference
                (args.Argument as MainWindow).CodeExecutionManager.Run();
            };
            worker.RunWorkerAsync(App.Window);
        }

        private void SettingsClick(object sender, MouseButtonEventArgs e)
        {
            new Graphics.Windows.Constructors.WindowPlacement(null, App.Window.MainCanvas, new Point(0, 0), new SettingsDisplay(App.Window.Settings));
        }
    }
}
