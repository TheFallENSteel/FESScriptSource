using System.Windows.Controls;
using System.ComponentModel;
using System.Xml.Serialization;

namespace FESScript.CodeWorks.BlockCreation.Blocks.UserElements.Contents.Args
{
    public struct ComboBoxArgs : IArgs
    {
        private string[] values;
        public string[] Values
        {
            get => values;
            set
            {
                values = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Values"));
            }
        }
        private int selectedIndex;
        [XmlAttribute]
        public int SelectedIndex
        {
            get => selectedIndex;
            set
            {
                selectedIndex = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedIndex"));
            }
        }
        [XmlIgnore] public int Value 
        { 
            get { return SelectedIndex; }
            set { SelectedIndex = value; }
        }

        public ComboBoxArgs(string[] values, int selectedIndex)
        {
            Values = values;
            SelectedIndex = selectedIndex;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public override string ToString() => nameof(ComboBox);
    }
}
