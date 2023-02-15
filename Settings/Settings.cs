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
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Controls.Primitives;
using System.Windows.Shapes;

namespace FESScript2.Settings
{
    public static class Settings
    {

        private static string directory = @"FESScriptFiles"; //= Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\FESScriptFiles";
        /// <summary>
        /// The path to directory of settings file.
        /// </summary>
        private static string pathToFile = Directory.GetCurrentDirectory() + @"\Settings";

        /// <summary>
        /// Name of the settings file.
        /// </summary>
        
        private static string fileName = @"Settings";

        /// <summary>
        /// Full path to settings file directory.
        /// </summary>

        private static string fullPath = pathToFile + @"\" + fileName + @".txt";

        /// <summary>
        /// Directory where the projects are saved. Returns null if ProjectDirectory doesn't exist. If is set and directory doesn't exist, directory is created.
        /// </summary>

        public static string DirectoryProperty
        { 
            get 
            {
                return directory;
            }
        }

        /// <summary>
        /// Saves settings into settings file.
        /// </summary>

        /*public static void SaveSettings() 
        {
            if (Directory.Exists(pathToFile)) 
            {
                Directory.CreateDirectory(pathToFile);
            }
            string[] file = new string[] 
            { 
                DirectoryProperty,
                Directories.SaveName
            };
            File.WriteAllLines(fullPath, file);
        }

        /// <summary>
        /// Loads Settings from the settings file.
        /// </summary>

        public static void LoadSettings() 
        {
            string[] settingsFile = File.ReadAllLines(fullPath);
            if (settingsFile.Length > 3)
            {
                directory = settingsFile[0];
                Directories.SaveName = settingsFile[1];
            }
        }*/
    }
}
