using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;
using FESScript.CodeWorks.BlockCreation.Interfaces;

namespace FESScript.CodeWorks.BlockCreation.Blocks.Templates
{
    public class DotTemplate(IFindable parent, Type type, IO iO, int iD, string name = "SampleDot", string description = "", string dotCopyCode = null) : IFindable, IInfo, INotifyPropertyChanged
    {
        [JsonIgnore] private IO iO = iO;
        [JsonIgnore] private Type type = type;
        [JsonIgnore] private string name = name;
        [JsonIgnore] private string description = description;
        [JsonIgnore] private IFindable parent = parent;
        [JsonIgnore] private int iD = iD;
        private string dotCopyCode = dotCopyCode;

        public int ID { get => iD; set { iD = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ID))); } }
        [JsonIgnore] public IFindable Parent { get => parent; set { parent = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Parent))); } }

        public string Name { get => name; set { name = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name))); } }
        public string Description { get => description; set { description = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Description))); } }
        public Type Type { get => type; set { type = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Type))); }}
        public IO IO { get => iO; set { iO = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IO))); }}
        public string DotCopyCode { get => dotCopyCode; set { dotCopyCode = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DotCopyCode))); }  }
        public DotTemplate(IFindable parent) : this(parent, Type.Error, IO.Error, -1) {}

        public void Update()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public IFindable FindChild(int ID) => null;
    }
}
