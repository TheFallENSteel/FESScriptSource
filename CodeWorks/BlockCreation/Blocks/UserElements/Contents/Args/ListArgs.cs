using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace FESScript.CodeWorks.BlockCreation.Blocks.UserElements.Contents.Args
{
    internal struct ListArgs<T> : IArgs
        where T : IArgs
    {
        public bool IsReadOnly { get; set; }
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

        public ListArgs(List<T> value, bool isReadOnly = false)
        {
            IsReadOnly = isReadOnly;
            Value = value;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
