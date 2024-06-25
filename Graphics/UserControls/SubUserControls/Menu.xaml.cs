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


namespace FESScript2.Graphics.UserControls.SubUserControls
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : UserControlPlus
    {
        public SupportWindow.Expander Expander;
        public Menu()
        {
            InitializeComponent();
            Expander = expander;
        }

        private void ConsoleStart()
        {
            MainWindow.MainConsole = new Console.Console();
            MainWindow.MainConsole.Show();
        }


        /// <summary>
        /// Button to save application.
        /// </summary>

        private void EllipseSave_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            CodeWorks.Saving.Saver.SaveProject(Block.blocks, Directories.SaveName);
        }

        /// <summary>
        /// Button to load application.
        /// </summary>

        private void EllipseLoad_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            CodeWorks.Saving.Loader.LoadProject(Directories.SaveName);
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
            CodeWorks.Transpiler.ProjectToCpp.GenerateCppFile(MainWindow.mainWindow.Start);
            CodeWorks.Transpiler.Compiler.CompileProject(Directories.SaveName, Directories.Directory);
        }
    }
}
