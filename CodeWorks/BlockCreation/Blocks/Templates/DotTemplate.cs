using FESScript.CodeWorks.BlockCreation.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FESScript.CodeWorks.BlockCreation.Blocks.Templates
{
    public class DotTemplate(Type type, IO iO, int iD, string name = "SampleDot", string description = "", string dotCopyCode = null) : IFindable, IInfo, INotifyPropertyChanged
    {
        [XmlIgnore] private IO iO = iO;
        [XmlIgnore] private Type type = type;
        [XmlIgnore] private string name = name;
        [XmlIgnore] private string description = description;
        [XmlIgnore] private IFindable parent;
        [XmlIgnore] private int iD = iD;
        private string dotCopyCode = dotCopyCode;

        [XmlAttribute] public int ID { get => iD; set { iD = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ID))); } }
        [XmlIgnore] public IFindable Parent { get => parent; set { parent = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Parent))); } }

        [XmlAttribute] public string Name { get => name; set { name = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name))); } }
        public string Description { get => description; set { description = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Description))); } }
        [XmlAttribute] public Type Type { get => type; set { type = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Type))); }}
        [XmlAttribute] public IO IO { get => iO; set { iO = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IO))); }}
        public string DotCopyCode { get => dotCopyCode; set { dotCopyCode = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DotCopyCode))); }  }
        public DotTemplate() : this(Type.Error, IO.Error, -1) { }

        public void Update()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public IFindable FindChild(int ID) => null;
    }
}
