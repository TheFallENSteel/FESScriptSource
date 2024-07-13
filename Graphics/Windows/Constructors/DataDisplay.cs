using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace FESScript.Graphics.Windows.Constructors
{
    public abstract class DataDisplay : UserControl
    {
        public abstract void SetData(object data);
        public abstract object GetData();
    }
}
