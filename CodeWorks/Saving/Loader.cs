using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using System.Xml.Serialization;
using FESScript2.Graphics.UserControls;
using FESScript2.Graphics.UserControls.SubUserControls;

namespace FESScript2.CodeWorks.Saving
{
    public static class Loader
    {
        private static Dictionary<int, Block> blocks;

        public static void LoadProject(string fileName)
        {
            Block.DestroyAll();
            blocks = new Dictionary<int, Block>();

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

                    LoadProject(projectData);

                    MainWindow.mainWindow.UpdateGlobalPosition();
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
        }
        private static void LoadProject(SaveProjectData projectData)
        {
            MainWindow.Zoom = projectData.Zoom;
            MainWindow.mainWindow.CameraPosition = new Point(projectData.CameraX, projectData.CameraY);

            for (int i = 0; i < projectData.Blocks.Count; i++)
            {
                LoadBlock(projectData.Blocks[i]);
            }
        }

        private static void LoadBlock(SaveBlockData blockData)
        {
            Block block = new Block(false, true);

            LoadProperties(blockData, ref block);
            LoadContentsData(blockData.ContentData, ref block);
            LoadDotConnections(blockData.DotData, ref block);

        }

        private static void LoadProperties(SaveBlockData blockData, ref Block block)
        {
            BlockCreation.BlockRecreation.RecreateBlock(BlockType.Find(blockData.BlockTypeID), out block);
            blocks.Add(blockData.ID, block);
            block.Position = new Point(blockData.PositionX, blockData.PositionY);
        }

        private static void LoadDotConnections(List<SaveDotData> dotData, ref Block block)
        {
            for (int i = 0; i < dotData.Count; i++)
            {
                LoadDotConnection(dotData[i], ref block);
            }
        }

        private static void LoadDotConnection(SaveDotData dotData, ref Block block)
        {
            try
            {
                if (dotData.ConnectedToParentID == -1 || dotData.ConnectedToID == -1) return; //No connection
                if (!blocks.ContainsKey(dotData.ConnectedToParentID)) return; //Is not the second block

                Connection connection = new Connection(block.FindDot(dotData.ID), blocks[dotData.ConnectedToParentID].FindDot(dotData.ConnectedToID));
            } 
            catch { }
        }

        private static void LoadContentsData(List<SaveContentData> contentData, ref Block block)
        {
            for (int i = 0; i < contentData.Count; i++)
            {
                LoadContentsData(contentData[i], ref block);
            }
        }
        private static void LoadContentsData(SaveContentData contentData, ref Block block)
        {
            IContents content = block.FindContent(contentData.ID);
            try 
            { 
                content.Text = contentData.Text;
            }
            catch { }
        }
    }
}
