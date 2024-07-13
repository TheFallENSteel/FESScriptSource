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
    internal struct BoolArgs : IArgs
    {
        private bool bValue;
        public bool Value
        {
            get => bValue;
            set
            {
                bValue = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Values"));
            }
        }

        public BoolArgs(bool value)
        {
            Value = value;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
