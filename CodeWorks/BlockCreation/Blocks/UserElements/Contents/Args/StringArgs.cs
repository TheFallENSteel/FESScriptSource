using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace FESScript.CodeWorks.BlockCreation.Blocks.UserElements.Contents.Args
{
    internal struct StringArgs : IArgs
    {
        private string sValue;
        public bool IsReadOnly { get; set; }
        public string Value
        {
            get => sValue;
            set
            {
                sValue = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value)));
            }
        }

        public StringArgs(string value, bool isReadOnly = false)
        {
            IsReadOnly = isReadOnly;
            Value = value;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void Set(IArgs args)
        {
            if (args is StringArgs stringArgs)
            {
                IsReadOnly = stringArgs.IsReadOnly;
                Value = stringArgs.Value;
            }
        }
    }
}
