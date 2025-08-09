using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using FESScript.CodeWorks.BlockCreation.Blocks;
using FESScript.CodeWorks.BlockCreation.Blocks.Templates;
using FESScript.CodeWorks.BlockCreation.Blocks.Placements;
using FESScript.CodeWorks.BlockCreation.Blocks.UserElements;
using Microsoft.Win32;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using FESScript.Graphics.UserControls;

namespace FESScript.CodeWorks.Saving
{
    public static class Loader
    {
        private static Dictionary<int, BlockPlacement> blockPlacements = new Dictionary<int, BlockPlacement>();

        public static void LoadProject(string path, MainWindow mainWindow, BlockTemplateManager blockTemplateManager)
        {
            BlockPlacement.RemoveAll();
            if (File.Exists(path))
            {
                try 
                {
                    SaveProjectData projectData = JsonSerializer.Deserialize<SaveProjectData>(File.ReadAllText(path));

                    LoadProject(projectData, mainWindow, blockTemplateManager);

                    App.Window.UpdateGlobalPosition();
                }
                catch
                { 
                    MessageBox.Show("Save file data is invalid.", "Error", MessageBoxButton.OK);
                }
            }
            else
            {
                MessageBox.Show("File not found. You need to create one before loading.", "Error", MessageBoxButton.OK);
            }
            blockPlacements.Clear();
        }
        private static void LoadTemplates(BlockTemplateManager blockTemplateManager)
        {
            //blockTemplateManager.LoadTemplates();
        }
        private static void LoadProject(SaveProjectData projectData, MainWindow mainWindow, BlockTemplateManager blockTemplateManager)
        {
            mainWindow.UserControlManager.Camera.SetZoom(projectData.Zoom);
            mainWindow.UserControlManager.Camera.CameraPosition = new Point(projectData.CameraX, projectData.CameraY);

            for (int i = 0; i < projectData.Blocks.Count; i++)
            {
                LoadBlock(projectData.Blocks[i], blockTemplateManager);
            }
        }

        private static void LoadBlock(SaveBlockData blockData, BlockTemplateManager blockTemplateManager)
        {

            BlockPlacement blockPlacement = new BlockPlacement(blockTemplateManager.GetTemplateById(blockData.BlockTemplateID), null, App.Window.mainCanvas);
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
                contentPlacement.Value.Set(contentData.Value);
            }
            catch { }
        }
    }
}
