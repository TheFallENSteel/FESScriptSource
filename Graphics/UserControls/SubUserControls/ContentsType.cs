using System;
using System.Collections.Generic;
using System.Text;

namespace FESScript2.Graphics.UserControls.SubUserControls
{

    /// <summary>
    /// Struct to reconstruct contents.
    /// </summary>

    public struct ContentsType : CodeWorks.IName
    {

        public ContentArgs.ContentArgs ContentArgs;

        public System.Type type;
        public int Id 
        { 
            get 
            {
                return id;
            }
            set
            {
                id = value;
            }
        }
        public bool isCompiler;
        private int id;
        public int collumn;
        public string text;

        public string Name 
        {
            get
            {
                string name = "E";
                if      (type == typeof(TextBox))
                {
                    name = "B";
                }
                else if (type == typeof(TextLabel))
                {
                    name = "L";
                }
                else if (type == typeof(Checkbox))
                {
                    name = "C";
                }
                else if (type == typeof(Combobox))
                {
                    name = "M";
                }
                return name + Id;
            }
        }
    }
}
