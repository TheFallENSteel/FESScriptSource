using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FESScript2.Graphics.UserControls;
using FESScript2.Graphics.UserControls.SubUserControls;

namespace FESScript2.CodeWorks.Saving
{
    public struct SaveProjectData
    {
        public double Version { get; set; }
        public double Zoom { get; set; }
        public double CameraX { get; set; }
        public double CameraY { get; set; }
        public List<SaveBlockData> Blocks { get; set; }
        public SaveProjectData(double zoom, Point cameraPosition, List<Block> blocks, double version) 
        { 
            Version = version;
            Zoom = zoom;
            CameraX = cameraPosition.X;
            CameraY = cameraPosition.Y;
            Blocks = blocks.Select(block => new SaveBlockData(block)).ToList();
        }
    }    
    public struct SaveBlockData
    {
        public int ID { get; set; }
        public int BlockTypeID { get; set; }
        public double PositionX { get; set; }
        public double PositionY { get; set; }
        public List<SaveDotData> DotData { get; set; }
        public List<SaveContentData> ContentData { get; set; }
        public SaveBlockData(Block block) 
        {
            BlockTypeID = block.blockType.ID;
            ID = block.ID;
            PositionX = block.Position.X;
            PositionY = block.Position.Y;
            DotData = block.dots.Select(s => new SaveDotData(s)).ToList();
            ContentData = block.contentsInteractive.Select(s => new SaveContentData(s)).ToList();
        }
    }
    public struct SaveDotData
    {
        public int ID { get; set; }
        public int ParentID { get; set; }

        public int ConnectedToID { get; set; }
        public int ConnectedToParentID { get; set; }

        public SaveDotData(Dots dot) 
        {
            ID = dot.ID;

            ParentID = dot.BlockParent.ID;
            if (dot.ConnectedTo != null) 
            { 
                ConnectedToID = dot.ConnectedTo.ID;
                ConnectedToParentID = dot.ConnectedTo.BlockParent.ID;
            }
            else 
            {
                ConnectedToID = -1;
                ConnectedToParentID = -1;
            }
        }
    }
    public struct SaveContentData
    {
        public int ID { get; set; }
        public string Text { get; set; }
        public SaveContentData(IContents content) 
        { 
            ID = content.ID;
            Text = content.Text;
        }
    }
}
