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
    internal struct ListArgs<T> : IArgs
    {
        private List<T> lValue;
        public List<T> Value
        {
            get => lValue;
            set
            {
                lValue = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value)));
            }
        }

        public ListArgs(List<T> value)
        {
            Value = value;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
