using System;
using System.Collections.Generic;
using System.Text;

namespace FESScript2.Graphics.UserControls.SubUserControls.ContentArgs
{
    public class ComboBoxArgs : ContentArgs
    {
        public ComboBoxArgs(List<string> elements) 
        {
            this.elements = elements;
        }
        public List<string> elements;
    }
}
