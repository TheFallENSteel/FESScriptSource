using System;
using System.Windows;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace FESScript2.CodeWorks.Saving
{
    public static class Loader
    {
        private static Dictionary<int, Graphics.UserControls.Block> blocks;

        public static void LoadProject (string fileName) 
        {
            Graphics.UserControls.Block.DestroyAll();
            blocks = new Dictionary<int, Graphics.UserControls.Block>();
            Directory.CreateDirectory(Directories.Projects + @$"\{fileName}");
            if (File.Exists(Directories.Projects + @$"\{fileName}\Save.FESSave")) 
            { 
                string[] file = File.ReadAllLines(Directories.Projects + @$"\{fileName}\Save.FESSave");
                string[] globalArgs = file[0].Split('|');
                MainWindow.Zoom = double.Parse(globalArgs[0]);
                MainWindow.mainWindow.CameraPosition = new Point(double.Parse(globalArgs[1]), double.Parse(globalArgs[2]));
                for (int i = 1; i < file.Length; i++)
                {
                    LoadBlock(file[i]);
                }
                MainWindow.mainWindow.UpdateGlobalPosition();
            }
            else 
            {
                MessageBox.Show("File not found. You need to create one before loading.", "Error", MessageBoxButton.OK);
            }
        }
        private static void LoadBlock(string line)
        {
            Graphics.UserControls.Block block = new Graphics.UserControls.Block();
            string[] parameters = line.Split("&");
            LoadProperties(parameters[0], ref block);
            LoadContentsData(parameters[2], ref block);
            LoadDotConnections(parameters[1], ref block);
            
        }
        private static void LoadProperties(string data, ref Graphics.UserControls.Block block) 
        {
            string[] args = data.Split("|");
            BlockCreation.BlockRecreation.RecreateBlock(Graphics.UserControls.BlockType.Find(int.Parse(args[0])), out block);
            blocks.Add(int.Parse(args[1]), block);
            block.Position = new System.Windows.Point(double.Parse(args[2]), double.Parse(args[3]));
        }
        private static void LoadDotConnections(string data, ref Graphics.UserControls.Block block)
        {
            string[] dataPacks = data.Split("/",StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < dataPacks.Length; i++) 
            { 
                string[] args = dataPacks[i].Split("|");
                int connectedToBlockId = int.Parse(args[1]);
                if (blocks.ContainsKey(connectedToBlockId)) 
                { 
                    Graphics.UserControls.SubUserControls.Dots dot11 = block.dots.Find((Graphics.UserControls.SubUserControls.Dots dot1) => dot1.Name == args[0]);
                    Graphics.UserControls.Connect connection = new Graphics.UserControls.Connect();
                    connection.AddDot(block.dots[block.dots.IndexOf(dot11)]);
                    Graphics.UserControls.SubUserControls.Dots dot22 = blocks[connectedToBlockId].dots.Find((Graphics.UserControls.SubUserControls.Dots dot2) => dot2.Name == args[2]);
                    try 
                    { 
                        connection.AddDot(blocks[connectedToBlockId].dots[blocks[connectedToBlockId].dots.IndexOf(dot22)]);
                    }
                    catch (Exception _){}
                }
            }
        }
        private static void LoadContentsData(string data, ref Graphics.UserControls.Block block)
        {
            string[] dataPacks = data.Split("/", StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < dataPacks.Length; i++)
            {
                string[] args = dataPacks[i].Split("|");
                Graphics.UserControls.SubUserControls.IContents content = block.contentsInteractive.Find((Graphics.UserControls.SubUserControls.IContents content) => content.Name == args[0]);
                content.Text = args[1];
            }
        }
    }
}
