using FESScript.Graphics.UserControls.SubUserControls;
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
using FESScript.CodeWorks.BlockCreation.Blocks.Templates;
using FESScript.CodeWorks.BlockCreation.Blocks.Placements;
using FESScript.CodeWorks.BlockCreation.Blocks.UserElements.Contents;
using FESScript.CodeWorks.BlockCreation.Blocks.UserElements;
using FESScript.Settings;


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
            BlockPlacement block = new BlockPlacement(
            new BlockTemplate(
                new System.Collections.ObjectModel.ObservableCollection<DotTemplate>()
                {
                        new DotTemplate(Type.Boolean, IO.Input, 4),
                        new DotTemplate(Type.Textual, IO.Output, 1),
                        new DotTemplate(Type.Console, IO.Output, 2),
                        new DotTemplate(Type.Numerical, IO.Input, 3),
                },
                new System.Collections.ObjectModel.ObservableCollection<ContentTemplate>()
                {
                        new ContentTemplate(typeof(CodeWorks.BlockCreation.Blocks.UserElements.Contents.TextBox), 4,"TextBox"),
                },
                Type.Numerical
                )
            {
            }, null, App.Window.mainCanvas);
            App.Window.MainConsole = new FESScript.CodeWorks.Console.Console();
            //MainWindow.MainConsole.Show();

        }


        /// <summary>
        /// Button to save application.
        /// </summary>

        private void EllipseSave_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            CodeWorks.Saving.Saver.SaveProject(BlockPlacement.blockPlacements, Directories.SaveName, App.Window);
        }

        /// <summary>
        /// Button to load application.
        /// </summary>

        private void EllipseLoad_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            CodeWorks.Saving.Loader.LoadProject(Directories.SaveName, App.Window);
        }

        /// <summary>
        /// Button to open settings window.
        /// </summary>

        /*private void EllipseSettings_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) 
        {
            Settings.Settings.LoadSettings();
        }*/

        /// <summary>
        /// Button to start application.
        /// </summary>

        private void EllipseStart_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //CodeWorks.Transpiler.Transpiler.TranspileProject(Settings.Settings.SaveName, Directories.Directory);
            ConsoleStart();
        }

        private void ellipseCompile_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //CodeWorks.Transpiler.ProjectToCpp.GenerateCppFile(MainWindow.mainWindow.Start);
            CodeWorks.Transpiler.Compiler.CompileProject(Directories.SaveName, Directories.Directory);
        }
    }
}
