using System;
using System.Collections.Generic;
using System.Text;

namespace FESScript.Graphics.UserControls.SubUserControls.ContentArgs
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
