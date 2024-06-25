using System;
using System.Collections.Generic;
using System.Text;

namespace FESScript2.Graphics.UserControls.SubUserControls
{

    /// <summary>
    /// Struct to reconstruct dots.
    /// </summary>

    public struct DotsType : CodeWorks.IName
    {
        public Type dotType;
        public IO io;
        public bool isConditional;
        public int ID {  get; set; }
        public string Name { get => (io == IO.Input ? "I" : (io == IO.Output ? "O" : "E")) + ID; }
    }
}
