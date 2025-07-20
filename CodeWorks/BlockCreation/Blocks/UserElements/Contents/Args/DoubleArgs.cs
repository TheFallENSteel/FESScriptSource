using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace FESScript.CodeWorks.BlockCreation.Blocks.UserElements.Contents.Args
{
    internal struct DoubleArgs : IArgs
    {
        public bool IsReadOnly { get; set; }
        private double dValue;
        public double Value
        {
            get => dValue;
            set
            {
                dValue = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Values"));
            }
        }

        public DoubleArgs(double value, bool isReadOnly = false)
        {
            IsReadOnly = isReadOnly;
            Value = value;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
