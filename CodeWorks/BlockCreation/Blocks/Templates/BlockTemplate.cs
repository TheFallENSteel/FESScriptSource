using FESScript.CodeWorks.BlockCreation.Blocks.Placements;
using FESScript.CodeWorks.BlockCreation.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace FESScript.CodeWorks.BlockCreation.Blocks.Templates
{
    public class BlockTemplate : IFindable, IInfo, IRemovable, INotifyPropertyChanged
    {
        [XmlIgnore] private static Dictionary<int, BlockTemplate> BlockTemplates = new Dictionary<int, BlockTemplate>();
        private int iD;
        private string name;
        private string description;
        private Type type;
        private string inFunctionBodyCode;
        private string inLineBodyCode;

        [XmlAttribute] public int ID 
        { 
            get { return iD; }
            set 
            { 
                iD = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ID)));
            }
        }

        [XmlIgnore] public IFindable Parent { get; }

        [XmlAttribute] public string Name
        {
            get => name; 
            set
            {
                name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
            }
        }
        public string Description
        {
            get => description;
            set
            {
                description = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Description)));
            }
        }

        [XmlAttribute] public Type Type
        {
            get => type; 
            set
            {
                type = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Type)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string InFunctionBodyCode
        {
            get => inFunctionBodyCode; 
            set
            {
                inFunctionBodyCode = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(InFunctionBodyCode)));
            }
        }
        public string InLineBodyCode
        {
            get => inLineBodyCode; 
            set
            {
                inLineBodyCode = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(InLineBodyCode)));
            }
        }

        public ObservableCollection<DotTemplate> Dots { get; init; }
        public ObservableCollection<ContentTemplate> Contents { get; init; }

        public BlockTemplate(ObservableCollection<DotTemplate> dots, ObservableCollection<ContentTemplate> contents, Type type, string name = "", string inFunctionBodyCode = null, string inLineBodyCode = null, string description = "")
        {
            this.Dots = dots;
            this.Contents = contents;
            this.InFunctionBodyCode = inFunctionBodyCode;
            this.InLineBodyCode = inLineBodyCode;
            this.Name = name;
            this.Type = type;
            this.Description = description;
            for (int i = 0; i < Dots.Count; i++)
            {
                Dots[0].Parent = this;
            }
            for (int i = 0; i < Contents.Count; i++)
            {
                Contents[i].Parent = this;
            }
            (this as IFindable).Register();
            BlockTemplates.Add(ID, this);
        }

        public IFindable FindChild(int ID)
        {
            ContentTemplate contentData = FindContent(ID);
            if (contentData != null) return contentData;
            DotTemplate dotData = FindDot(ID);
            if (dotData != null) return dotData;
            return null;
        }

        public ContentTemplate FindContent(int ID)
        {
            return Contents.First((element) => element.ID == ID);
        }

        public DotTemplate FindDot(int ID)
        {
            return Dots.First((element) => element.ID == ID);
        }

        public void Update() 
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
        }

        public void Remove()
        {
            (this as IFindable).UnRegister();
            BlockTemplates.Remove(ID);
            Dots.Clear();
            Contents.Clear();
        }
        public static BlockTemplate GetTemplate(int id) 
        { 
            return BlockTemplates[id];
        }
    }
}
