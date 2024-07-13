using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FESScript.Graphics.UserControls.SubUserControls;
using System.Xml.Serialization;
using FESScript.CodeWorks.BlockCreation.Blocks.Placements;
using FESScript.CodeWorks.Functions;
using FESScript.CodeWorks.BlockCreation.Interfaces;
using FESScript.CodeWorks.BlockCreation.Blocks.UserElements.Contents.Args;
using System.Windows.Media.Animation;

namespace FESScript.CodeWorks.Saving
{
    public struct SaveProjectData
    {
        [XmlAttribute]
        public double Version { get; set; }
        [XmlAttribute]
        public double Zoom { get; set; }
        [XmlAttribute]
        public double CameraX { get; set; }
        [XmlAttribute]
        public double CameraY { get; set; }
        [XmlAttribute]
        public int LargestUniqueID 
        {
            get => IFindable.LargestID;
            set => IFindable.LargestID = value;
        }
        [XmlElement]
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
    public struct SaveBlockData
    {
        [XmlAttribute]
        public int ID { get; set; }
        [XmlAttribute]
        public int BlockTemplateID { get; set; }
        [XmlAttribute]
        public double PositionX { get; set; }
        [XmlAttribute]
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
        [XmlAttribute]
        public int ID { get; set; }
        [XmlAttribute]
        public int ParentID { get; set; }

        [XmlAttribute]
        public int ConnectedToID { get; set; }
        [XmlAttribute]
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
        [XmlAttribute]
        public int ID { get; set; }
        [XmlElement("Text", Type = typeof(StringArgs))]
        [XmlElement("Boolean", Type = typeof(BoolArgs))]
        [XmlElement("Number", Type = typeof(DoubleArgs))]
        [XmlElement("Arguments", Type = typeof(ListArgs<string>))]
        [XmlElement("ComboBoxArgs", Type = typeof(ComboBoxArgs))]
        public IArgs Value { get; set; }
        public SaveContentData(ContentPlacement content) 
        { 
            ID = content.ID;
            Value = content.Value;
        }
    }
}
