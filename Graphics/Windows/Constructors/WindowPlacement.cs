using FESScript.CodeWorks.BlockCreation.Blocks;
using FESScript.CodeWorks.BlockCreation.Interfaces;
using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using FESScript.Graphics.Windows.Displays;
using FESScript.CodeWorks.BlockCreation.Blocks.Templates;

namespace FESScript.Graphics.Windows.Constructors
{
    public class WindowPlacement : CanvasItem, IFindable
    {
        public Window Window { get; private set; }
        public override UserControl Item { get => Window;  }
        public IFindable Parent { get; private set; }
        public int ID { get; set; }

        public WindowPlacement(IFindable parent, Panel container, Point Position, DataDisplay dataDisplay) : base(container, Position)
        {
            this.Parent = parent;
            this.Window = new Window(this, dataDisplay);
            (this as IFindable).Register();
            this.EventSubscribe();
            this.Show();
        }

        public void Remove() 
        { 
            (this as IFindable).UnRegister();
        }

        public void Highlight() 
        {
            this.Window.DoHighlight();
        }

        public IFindable FindChild(int ID) => null;
    }

    public struct DataTableElement
    {
        
    }
}
