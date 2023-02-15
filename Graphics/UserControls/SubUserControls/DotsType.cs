using System;
using System.Collections.Generic;
using System.Text;

namespace FESScript2.Graphics.UserControls.SubUserControls
{

    /// <summary>
    /// Struct ro reconstruct dots.
    /// </summary>

    public struct DotsType : CodeWorks.IName
    {
        public Type dotType;
        public IO io;
        public bool isConditional;
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
        private int id;

        public string Name
        {
            get
            {
                return (io == IO.Input ? "I" : (io == IO.Output ? "O" : "E")) + Id;
            }
        }
    }
}
