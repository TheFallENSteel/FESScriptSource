using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Windows;
using FESScript.CodeWorks.BlockCreation.Blocks.Placements;
using FESScript.Settings;

namespace FESScript.CodeWorks.Saving
{
    public static class Saver
    {
        public static void SaveProject(List<BlockPlacement> blockPlacements, string fileName, MainWindow mainWindow)
        {
            try 
            { 
                SaveProjectData projectData = new SaveProjectData(mainWindow.Zoom, App.Window.CameraPosition, blockPlacements, MainWindow.Version);

                string path = Directories.Projects + @$"\{fileName}";
                Directory.CreateDirectory(path);
                using (Stream stream = File.Open(path + @"\Save.FESSave", FileMode.Create)) 
                { 
                    XmlSerializer serializer = new XmlSerializer(typeof(SaveProjectData));
                    serializer.Serialize(stream, projectData);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Cannot save file: {ex.Message}", "Error", MessageBoxButton.OK);
            }
        }
    }
}
