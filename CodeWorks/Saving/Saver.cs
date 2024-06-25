using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Windows;

namespace FESScript2.CodeWorks.Saving
{
    public static class Saver
    {
        public static void SaveProject(List<Graphics.UserControls.Block> blocks, string fileName)
        {
            try 
            { 
                SaveProjectData projectData = new SaveProjectData(MainWindow.Zoom, MainWindow.mainWindow.CameraPosition, blocks, MainWindow.Version);

                string path = Directories.Projects + @$"\{fileName}";
                Directory.CreateDirectory(path);
                Stream stream = File.Open(path + @"\Save.FESSave", FileMode.OpenOrCreate);
                XmlSerializer serializer = new XmlSerializer(typeof(SaveProjectData));
                serializer.Serialize(stream, projectData);
                stream.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Cannot save file: {ex.Message}", "Error", MessageBoxButton.OK);
            }
        }
    }
}
