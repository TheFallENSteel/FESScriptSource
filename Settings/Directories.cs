using System;
using System.Windows;
using System.Collections.Generic;
using System.Text;

namespace FESScript2
{
    public static class Directories
    {
        const string projectPath = @"AppFiles\Projects";
        const string libraryPath = @"AppFiles\Blocks\Precompiled";
        const string defBlockFolder = @"AppFiles\Blocks\Design";
        public const string programName = "Program";
        
        public static string Directory 
        { 
            get 
            {
                System.IO.Directory.CreateDirectory(Settings.Settings.DirectoryProperty);
                return Settings.Settings.DirectoryProperty;
            }
        }

        public static string SaveName 
        { 
            get 
            {
                return MainWindow.SaveName;
            }
            set
            {
                try 
                { 
                    MainWindow.SaveName = value;
                }
                catch 
                { 
                    
                }
            }
        }

        

        public static string SafeFilePathCombination(string firstPath, string secondPath) 
        {
            string returnValue = firstPath + @"\" + secondPath;
            System.IO.Directory.CreateDirectory(returnValue);
            return returnValue;
        }

        public static string PrecompiledFiles 
        { 
            get 
            {
                System.IO.Directory.CreateDirectory($@"{Directory}\{libraryPath}");
                return $@"{Directory}\{libraryPath}";
            }
        }
        public static string Projects
        {
            get
            {
                System.IO.Directory.CreateDirectory($@"{Directory}\{projectPath}");
                return $@"{Directory}\{projectPath}";
            }
        }
        public static string Blocks
        {
            get
            {
                System.IO.Directory.CreateDirectory($@"{Directory}\{defBlockFolder}");
                return $@"{Directory}\{defBlockFolder}";
            }
        }
    }
}
