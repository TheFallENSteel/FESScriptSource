using FESScript.CodeWorks.BlockCreation.Blocks.Placements;
using FESScript.CodeWorks.Functions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using FESScript.CodeWorks.BlockCreation.Blocks;

namespace FESScript.CodeWorks.BlockCreation.Interfaces
{
    public abstract class CanvasItem : IMoveable
    {
        public abstract UserControl Item { get;  }

        public Point Position { get; set; }
        public bool IsClicked { get; set; }
        public Point Offset { get; set; }
        public Panel Container { get; set; }

        public EventHandler OnMove;

        public CanvasItem(Panel container, Point position)
        {
            this.Position = position;
            this.IsClicked = false;
            this.Offset = new Point(0, 0);
            this.Container = container;
        }

        public virtual void Move(double x, double y, bool relativeToScreen = true)
        {
            if (relativeToScreen)
            {
                Position = new Point(Position.X + x, Position.Y + y);
            }
            else
            {
                Point cameraPosition = (App.Current.MainWindow as MainWindow).UserControlManager.Camera.CameraPosition;
                Position = new Point(x + cameraPosition.X, y + cameraPosition.Y);
            }
            DrawPosition();
        }

        public virtual void DrawPosition()
        {
            Point cameraPosition = (App.Current.MainWindow as MainWindow).UserControlManager.Camera.CameraPosition;
            Canvas.SetLeft(Item, Position.X - cameraPosition.X);
            Canvas.SetTop(Item, Position.Y - cameraPosition.Y);
            OnMove?.Invoke(null, null);
        }

        public virtual void EventSubscribe()
        {
            Item.MouseDown += (o, e) => ((IMoveable)this).MouseDown(o, e);
            Item.MouseMove += (o, e) => ((IMoveable)this).MouseMove(o, e);
            Item.SizeChanged += (_, _) => OnMove?.Invoke(null, null);
            Item.Focusable = true;
            (App.Current.MainWindow as MainWindow).UserControlManager.Camera.CameraMoveEvent += (o, e) => ((IMoveable)this).Redraw(o, e);
        }

        public void Show()
        {
            if (Container != null) Container.Children.Add(Item);
        }

        public void Hide()
        {
            if (Container != null) Container.Children.Remove(Item);
        }
    }
}
