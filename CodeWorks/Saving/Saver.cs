using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Runtime.Serialization;
using System.Windows;
using FESScript.CodeWorks.BlockCreation.Blocks.Placements;

namespace FESScript.CodeWorks.Saving
{
    public static class Saver
    {
        public static void SaveProject(List<BlockPlacement> blockPlacements, string path, MainWindow mainWindow)
        {
            try 
            { 
                SaveProjectData projectData = new SaveProjectData(mainWindow.Zoom, App.Window.CameraPosition, blockPlacements, MainWindow.Version);

                using (Stream stream = File.Open(path, FileMode.Create)) 
                { 
                    JsonSerializer.Serialize<SaveProjectData>(stream, projectData, new JsonSerializerOptions(JsonSerializerDefaults.General) { WriteIndented = true });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Cannot save file: {ex.Message}, {ex.ToString()}", "Error", MessageBoxButton.OK);
            }
        }
    }
}
