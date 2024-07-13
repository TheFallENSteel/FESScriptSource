using FESScript.CodeWorks.BlockCreation.Blocks.UserElements.Contents;
using FESScript.CodeWorks.BlockCreation.Blocks.UserElements.Contents.Args;
using FESScript.CodeWorks.BlockCreation.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;

namespace FESScript.CodeWorks.BlockCreation.Blocks.Templates
{
    public class ContentTemplate(System.Type type, int iD, string name = "SampleContent", IArgs value = null, string description = null) : IFindable, IInfo, INotifyPropertyChanged
    {
        [XmlIgnore] private System.Type type = type;
        [XmlIgnore] private string name = name;
        [XmlIgnore] private string description = description;
        [XmlIgnore] private IFindable parent;
        [XmlIgnore] private int iD = iD;
        [XmlIgnore] private IArgs initialValue = value;

        [XmlAttribute] public int ID { get => iD; set { iD = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ID))); } }
        [XmlIgnore] public IFindable Parent { get => parent; set { parent = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Parent))); } }

        [XmlAttribute] public IArgs InitialValue { get => initialValue; set { initialValue = value; PropertyChanged?.Invoke( this, new PropertyChangedEventArgs(nameof(InitialValue))); } }

        [XmlAttribute] public string Name { get => name; set { name = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name))); } }
        public string Description { get => description; set { description = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Description))); } }

        [XmlAttribute] public System.Type Type { get => type; set { type = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Type))); } }

        public void Update()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
        }

        public static readonly ObservableCollection<System.Type> ContentTemplates = [
            typeof(Empty),
            typeof(TextLabel),
            typeof(TextBox),
            typeof(CheckBox),
            typeof(Combobox),
        ];

        public ContentTemplate() : this(ContentTemplates[0], -1) { }

        public event PropertyChangedEventHandler PropertyChanged;

        public IFindable FindChild(int ID) => null;
    }
}
