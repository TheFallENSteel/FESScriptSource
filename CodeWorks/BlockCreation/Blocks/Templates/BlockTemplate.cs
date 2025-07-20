using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text.Json.Serialization;
using FESScript.CodeWorks.BlockCreation.Blocks.Placements;
using FESScript.CodeWorks.BlockCreation.Interfaces;

namespace FESScript.CodeWorks.BlockCreation.Blocks.Templates
{
    public class BlockTemplate : IFindable, IInfo, IRemovable, INotifyPropertyChanged
    {
        [JsonIgnore] private static Dictionary<int, BlockTemplate> BlockTemplates = new Dictionary<int, BlockTemplate>();
        private int iD;
        private string name;
        private string description;
        private Type type;
        private string inFunctionBodyCode;
        private string inLineBodyCode;

        public int ID 
        { 
            get { return iD; }
            set 
            { 
                iD = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ID)));
            }
        }

        [JsonIgnore] public IFindable Parent { get; }

        public string Name
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

        public Type Type
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
        public string InBlockCode
        {
            get => inLineBodyCode; 
            set
            {
                inLineBodyCode = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(InBlockCode)));
            }
        }

        public ObservableCollection<DotTemplate> Dots { get; init; }
        public ObservableCollection<ContentTemplate> Contents { get; init; }

        public void AddDot(DotTemplate dot)
        {
            Dots.Add(dot);
            dot.Parent = this;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Dots)));
        }
        public void AddContent(ContentTemplate content)
        {
            Contents.Add(content);
            content.Parent = this;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Contents)));
        }
        public void RemoveDot(DotTemplate dot)
        {
            Dots.Remove(dot);
            dot.Parent = null;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Dots)));
        }
        public void RemoveContent(ContentTemplate content)
        {
            Contents.Remove(content);
            content.Parent = null;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Contents)));
        }
        public BlockTemplate(ObservableCollection<DotTemplate> dots, ObservableCollection<ContentTemplate> contents, Type type, string name = "", string inFunctionBodyCode = null, string inLineBodyCode = null, string description = "")
        {
            this.Dots = dots;
            this.Contents = contents;
            this.InFunctionBodyCode = inFunctionBodyCode;
            this.InBlockCode = inLineBodyCode;
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
        public static BlockTemplate CreateEmptyTemplate() => 
            new BlockTemplate(
                new ObservableCollection<DotTemplate>(), 
                new ObservableCollection<ContentTemplate>(), 
                Type.Error, 
                "New Template", 
                "", 
                "", 
                "Empty description");

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

        public void Update(object sender, PropertyChangedEventArgs args) 
        {
            PropertyChanged?.Invoke(sender, args);
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
