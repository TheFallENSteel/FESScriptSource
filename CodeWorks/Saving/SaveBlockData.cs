using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FESScript.Graphics.UserControls.SubUserControls;
using FESScript.CodeWorks.BlockCreation.Blocks.Placements;
using FESScript.CodeWorks.Functions;
using FESScript.CodeWorks.BlockCreation.Interfaces;
using FESScript.CodeWorks.BlockCreation.Blocks.UserElements.Contents.Args;
using System.Windows.Media.Animation;
using FESScript.CodeWorks.BlockCreation.Blocks.Templates;

namespace FESScript.CodeWorks.Saving
{
    public struct SaveProjectData
    {
        public double Version { get; set; }
        public double Zoom { get; set; }
        public double CameraX { get; set; }
        public double CameraY { get; set; }
        public int LargestUniqueID 
        {
            get => IFindable.LargestID;
            set => IFindable.LargestID = value;
        }
        public List<int> FreeIDs
        {
            get => IFindable.freeID.ToList();
            set => IFindable.freeID = new Queue<int>(value);
        }

        public List<SaveBlockData> Blocks { get; set; }
        public SaveProjectData(double zoom, Point cameraPosition, List<BlockPlacement> blockPlacements, double version) 
        { 
            Version = version;
            Zoom = zoom;
            CameraX = cameraPosition.X;
            CameraY = cameraPosition.Y;
            Blocks = blockPlacements.Select(blockPlacement => new SaveBlockData(blockPlacement)).ToList();
        }
    }    
    public struct SaveDotTemplateData
    {
        public int ID { get; set; }
        public int ParentID { get; set; }
        public SaveDotTemplateData(DotTemplate dotTemplate) 
        {
            ID = dotTemplate.ID;
            ParentID = dotTemplate.Parent.ID;
        }
    }
    public struct SaveBlockData
    {
        public int ID { get; set; }
        public int BlockTemplateID { get; set; }
        public double PositionX { get; set; }
        public double PositionY { get; set; }
        public List<SaveDotData> DotData { get; set; }
        public List<SaveContentData> ContentData { get; set; }
        public SaveBlockData(BlockPlacement blockPlacement) 
        {
            BlockTemplateID = blockPlacement.BlockTemplate.ID;
            ID = blockPlacement.ID;
            PositionX = blockPlacement.Position.X;
            PositionY = blockPlacement.Position.Y;
            DotData = blockPlacement.DotPlacements.Select(s => new SaveDotData(s)).ToList();
            ContentData = blockPlacement.ContentPlacements.Select(s => new SaveContentData(s)).ToList();
        }
    }
    public struct SaveDotData
    {
        public int ID { get; set; }
        public int ParentID { get; set; }

        public int ConnectedToID { get; set; }
        public int ConnectedToParentID { get; set; }

        public SaveDotData(DotPlacement dotPlacement) 
        {
            ID = dotPlacement.DotTemplate.ID;

            ParentID = dotPlacement.DotTemplate.Parent.ID;
            if (dotPlacement.ConnectedTo != null) 
            { 
                ConnectedToID = dotPlacement.ConnectedTo.ID;
                ConnectedToParentID = dotPlacement.ConnectedTo.Parent.ID;
            }
            else 
            {
                ConnectedToID = 0;
                ConnectedToParentID = 0;
            }
        }
    }
    public struct SaveContentData
    {
        public int ID { get; set; }
        public IArgs Value { get; set; }
        public SaveContentData(ContentPlacement content) 
        { 
            ID = content.ContentData.ID;
            if(!content.Value.IsReadOnly) Value = content.Value;
        }
    }
}
