using FESScript.CodeWorks.BlockCreation.Blocks.Placements;
using FESScript.CodeWorks.BlockCreation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace FESScript.CodeWorks.BlockCreation.Blocks.UserElements.Commands
{
    public static class BlockCommands
    {
        public static void Delete(BlockPlacement parameter)
        {
            parameter.Remove();
        }
        public static void Duplicate(BlockPlacement parameter)
        {
            parameter.Clone();
        }
        public static void Reset(BlockPlacement parameter)
        {
            parameter.UpdateTemplate(parameter, new System.ComponentModel.PropertyChangedEventArgs("all"));
        }
    }
}
