using System.Windows.Controls;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace FESScript.CodeWorks.BlockCreation.Blocks.UserElements.Contents.Args
{
    public struct ComboBoxArgs : IArgs
    {
        public bool IsReadOnly { get; set; }
        private string[] values;
        [JsonIgnore]
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
        public int SelectedIndex
        {
            get => selectedIndex;
            set
            {
                selectedIndex = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedIndex"));
            }
        }
        [JsonIgnore]
        public int Value
        {
            get { return SelectedIndex; }
            set { SelectedIndex = value; }
        }

        public ComboBoxArgs(string[] values, int selectedIndex, bool isReadOnly = false)
        {
            IsReadOnly = isReadOnly;
            Values = values;
            SelectedIndex = selectedIndex;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public override string ToString() => nameof(ComboBox);
        public void Set(IArgs args)
        {
            if (args is ComboBoxArgs comboBoxArgs)
            {
                IsReadOnly = comboBoxArgs.IsReadOnly;
                if (comboBoxArgs.values != null) Values = comboBoxArgs.Values;
                SelectedIndex = comboBoxArgs.SelectedIndex;
            }
        }
    }
}
