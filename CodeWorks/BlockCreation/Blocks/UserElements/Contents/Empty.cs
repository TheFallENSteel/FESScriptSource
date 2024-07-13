using FESScript.CodeWorks.BlockCreation.Blocks.Placements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace FESScript.CodeWorks.BlockCreation.Blocks.UserElements.Contents
{
    public class Empty : UserControl
    {
        public Empty(ContentPlacement _) 
        { 

        }
        public override string ToString() => nameof(Empty);
    }
}
