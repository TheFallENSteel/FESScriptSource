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
    internal struct DoubleArgs : IArgs
    {
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

        public DoubleArgs(double value)
        {
            Value = value;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
