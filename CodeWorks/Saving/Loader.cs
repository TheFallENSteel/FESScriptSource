using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using System.Xml.Serialization;
using FESScript.CodeWorks.BlockCreation.Blocks;
using FESScript.CodeWorks.BlockCreation.Blocks.Templates;
using FESScript.CodeWorks.BlockCreation.Blocks.Placements;
using FESScript.CodeWorks.BlockCreation.Blocks.UserElements;
using FESScript.Graphics.UserControls.Connecting;
using FESScript.Settings;

namespace FESScript.CodeWorks.Saving
{
    public static class Loader
    {
        private static Dictionary<int, BlockPlacement> blockPlacements = new Dictionary<int, BlockPlacement>();

        public static void LoadProject(string fileName, MainWindow mainWindow)
        {
            BlockPlacement.RemoveAll();

            string DirUri = Directories.Projects + @$"\{fileName}";
            string FileUri = DirUri + @"\Save.FESSave";

            Directory.CreateDirectory(DirUri);


            if (File.Exists(FileUri))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(SaveProjectData));
                XmlReader reader = XmlReader.Create(FileUri);
                try 
                { 
                    SaveProjectData projectData = (SaveProjectData)serializer.Deserialize(reader);

                    LoadProject(projectData, mainWindow);

                    App.Window.UpdateGlobalPosition();
                }
                catch 
                { 
                    MessageBox.Show("Save file data is invalid.", "Error", MessageBoxButton.OK);
                }
                reader.Close();
            }
            else
            {
                MessageBox.Show("File not found. You need to create one before loading.", "Error", MessageBoxButton.OK);
            }
            blockPlacements.Clear();
        }
        private static void LoadProject(SaveProjectData projectData, MainWindow mainWindow)
        {
            mainWindow.Zoom = projectData.Zoom;
            mainWindow.CameraPosition = new Point(projectData.CameraX, projectData.CameraY);

            for (int i = 0; i < projectData.Blocks.Count; i++)
            {
                LoadBlock(projectData.Blocks[i]);
            }
        }

        private static void LoadBlock(SaveBlockData blockData)
        {

            BlockPlacement blockPlacement = new BlockPlacement(BlockTemplate.GetTemplate(blockData.BlockTemplateID), null, App.Window.mainCanvas);
            LoadProperties(blockData, ref blockPlacement);
            LoadContentsData(blockData.ContentData, ref blockPlacement);
            LoadDotConnections(blockData.DotData, ref blockPlacement);

        }

        private static void LoadProperties(SaveBlockData blockData, ref BlockPlacement blockPlacement)
        {   
            blockPlacements.Add(blockData.ID, blockPlacement);
            blockPlacement.Position = new Point(blockData.PositionX, blockData.PositionY);
        }

        private static void LoadDotConnections(List<SaveDotData> dotData, ref BlockPlacement blockPlacement)
        {
            for (int i = 0; i < dotData.Count; i++)
            {
                LoadDotConnection(dotData[i], ref blockPlacement);
            }
        }

        private static void LoadDotConnection(SaveDotData dotData, ref BlockPlacement blockPlacement)
        {
            try
            {
                if (dotData.ConnectedToParentID == 0 || dotData.ConnectedToID == 0) return; //No connection
                if (!blockPlacements.ContainsKey(dotData.ConnectedToParentID)) return; //Is not the second block

                Connection connection = new Connection(
                    blockPlacement.FindDot(dotData.ID), 
                    blockPlacements[dotData.ConnectedToParentID].FindDot(dotData.ConnectedToID));
            } 
            catch { }
        }

        private static void LoadContentsData(List<SaveContentData> contentData, ref BlockPlacement blockPlacement)
        {
            for (int i = 0; i < contentData.Count; i++)
            {
                LoadContentsData(contentData[i], ref blockPlacement);
            }
        }
        private static void LoadContentsData(SaveContentData contentData, ref BlockPlacement blockPlacement)
        {
            try 
            { 
                ContentPlacement contentPlacement = blockPlacement.FindContent(contentData.ID);
                contentPlacement.Value = contentData.Value;
            }
            catch { }
        }
    }
}
