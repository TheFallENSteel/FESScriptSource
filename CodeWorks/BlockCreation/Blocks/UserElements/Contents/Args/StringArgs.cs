using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml.Serialization;

namespace FESScript.CodeWorks.BlockCreation.Blocks.UserElements.Contents.Args
{
    internal struct StringArgs : IArgs
    {
        private string sValue;
        public string Value
        {
            get => sValue;
            set
            {
                sValue = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value)));
            }
        }

        public StringArgs(string value)
        {
            Value = value;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
