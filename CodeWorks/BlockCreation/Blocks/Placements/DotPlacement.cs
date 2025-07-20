using FESScript.CodeWorks.BlockCreation.Blocks.Templates;
using FESScript.CodeWorks.BlockCreation.Interfaces;
using FESScript.CodeWorks.BlockCreation.Blocks.UserElements;
using System.Windows.Input;
using FESScript.Graphics.UserControls;

namespace FESScript.CodeWorks.BlockCreation.Blocks.Placements
{
    public class DotPlacement : IFindable, IInfo, IRemovable
    {
        public DotTemplate DotTemplate { get; set; }

        public Connection Connection { get; set; } = new Connection();

        public Dot Dot { get; set; }

        public DotPlacement ConnectedTo { get => Connection.ConnectedTo(this); }

        public IO IO { get => DotTemplate.IO; }
        public Type Type { get => DotTemplate.Type; }
        public IFindable Parent { get; set; }
        public int ID { get; set; }
        public string Name { get => DotTemplate.Name; }
        public string Description { get => DotTemplate.Description; }

        public DotPlacement(DotTemplate dotData, IFindable parent)
        {
            DotTemplate = dotData;
            Parent = parent;
            ID = DotTemplate.ID;
            this.Dot = new Dot(this) { MouseDownHandler = MouseDown, MouseUpHandler = MouseUp };
        }

        public void Remove() 
        { 
            this.Connection.BreakConnection();
            this.Dot.Remove();
        }

        public void MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Connection.CurrentConnection == null) Connection.StartConnection(this);
        }
        public void MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (Connection.CurrentConnection != null) Connection.CurrentConnection.FinishConnection(this);
        }
        public string FullName() => $"{(Parent as BlockPlacement).FullName()}_D{IO.ToString()}{ID.ToString()})";

        public IFindable FindChild(int ID)
        {
            return null;
        }
    }
}
