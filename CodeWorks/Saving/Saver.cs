using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace FESScript2.CodeWorks.Saving
{
    public static class Saver
    {
        public static void SaveProject(List<Graphics.UserControls.Block> blocks, string fileName)
        {
            string savingString = @$"{MainWindow.Zoom}|{MainWindow.mainWindow.CameraPosition.X}|{MainWindow.mainWindow.CameraPosition.Y}
";
            foreach (Graphics.UserControls.Block block in blocks)
            {
                SaveBlock(block, ref savingString);
            }
            string path = Directories.Projects + @$"\{fileName}";
            Directory.CreateDirectory(path);
            File.WriteAllText(path + @"\Save.FESSave", savingString);
        }

        private static void SaveBlock(Graphics.UserControls.Block block, ref string savingString)
        {
            savingString += block.blockType.id + "|";
            savingString += block.Id + "|";
            savingString += block.Position.X + "|";
            savingString += block.Position.Y + "&";
            for(int i = 0; i < block.dots.Count; i++) 
            {
                if (block.dots[i].ConnectedTo != null) 
                { 
                    savingString += block.dots[i].Name + "|" + block.dots[i].ConnectedTo.BlockParent.Id + "|" + block.dots[i].ConnectedTo.Name + ((i + 1 == block.dots.Count) ? "" : "/");
                }
            }
            savingString += "&";
            for (int i = 0; i < block.contentsInteractive.Count; i++)
            {
                savingString += block.contentsInteractive[i].Name + "|" + block.contentsInteractive[i].Text + ((i + 1 == block.contentsInteractive.Count) ? "" : "/");
            }
            savingString += "\n";
        }
    }
}
