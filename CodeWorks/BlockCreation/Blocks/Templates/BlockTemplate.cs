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
        private int iD;
        private string name;
        private string description;
        private Type type;
        private string inLineBodyCode;
        public ObservableCollection<DotTemplate> Dots { get; set; }
        public ObservableCollection<ContentTemplate> Contents { get; set; }

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

        public void Update()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public string InBlockCode
        {
            get => inLineBodyCode; 
            set
            {
                inLineBodyCode = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(InBlockCode)));
            }
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

        public void Update(object sender, PropertyChangedEventArgs args) 
        {
            PropertyChanged?.Invoke(sender, args);
        }

        public void Remove()
        {
            (this as IFindable).UnRegister();
            Dots.Clear();
            Contents.Clear();
        }
    }
}
