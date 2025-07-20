using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Input;
using FESScript.CodeWorks.Functions;
using FESScript.CodeWorks.BlockCreation.Blocks.Templates;
using FESScript.CodeWorks.BlockCreation.Blocks.Placements;

namespace FESScript.Graphics.UserControls
{
    public class Connection
    {

        public static Connection CurrentConnection;
        
        DotPlacement[] dots = new DotPlacement[2];

        Line Connector { get; set; }

        public Connection()
        {
            Construct();
        }

        private void Construct(DotPlacement dot1 = null, DotPlacement dot2 = null) 
        {
            this.Connector = new Line()
            {
                StrokeThickness = 5,
                StrokeStartLineCap = PenLineCap.Round,
                StrokeEndLineCap = PenLineCap.Round,
                IsHitTestVisible = true,
            };
            dots[0] = dot1;
            dots[1] = dot2;
            if(dots[0] != null) Connector.Stroke = ColorsBrushes.TypeToBrush[dots[0].DotTemplate.Type];
            this.Connector.MouseDown += OnMouseClick;
        }

        public Connection(DotPlacement dot1, DotPlacement dot2)
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                App.Window.mainCanvas.Children.Add(this.Connector);
                OnUpdate(null, null);
            }), System.Windows.Threading.DispatcherPriority.Loaded);
            Construct(dot1, dot2);
            ConnectionCreation();
        }

        public DotPlacement[] Dots 
        { 
            get => this.dots; 
            set => this.dots = value; 
        }

        public void StartConnection(DotPlacement dot)
        {
            BreakConnection();
            App.Window.mainCanvas.Children.Add(this.Connector);
            CurrentConnection = this;
            IMoveable.IsMovable = false;
            dots[0] = dot;
            Connector.IsHitTestVisible = false;
            Connector.Stroke = ColorsBrushes.TypeToBrush[dot.DotTemplate.Type];
        }

        public void ContinueConnection(Point MousePosition) 
        { 
            ChangePosition(dots[0].Dot.Connector.X, dots[0].Dot.Connector.Y, MousePosition.X, MousePosition.Y);
        }

        public void FinishConnection(DotPlacement dot)
        {
            IMoveable.IsMovable = true;
            if (dot != null && CurrentConnection.CanConnect(CurrentConnection.dots[0], dot))
            {
                dot.Connection.BreakConnection();
                CurrentConnection.dots[1] = dot;
                CurrentConnection.dots[1].Connection = CurrentConnection;
                ConnectionCreation();
            }
            else
            {
                Connector.IsHitTestVisible = true;
                BreakConnection();
            }
            CurrentConnection = null;
        }

        private void ConnectionCreation() 
        { 
            foreach (DotPlacement dot in dots) 
            {
                if (dot != null)
                {
                    dot.Dot.Connector.PropertyChanged += OnUpdate;
                    dot.Connection = this;
                }
            }
            Connector.IsHitTestVisible = true;
            OnUpdate(null, null);
        }

        public void BreakConnection() 
        {
            App.Window.mainCanvas.Children.Remove(this.Connector);
            foreach (DotPlacement dot in dots)
            {
                if (dot!=null) 
                { 
                    dot.Dot.Connector.PropertyChanged -= OnUpdate;
                    dot.Connection = new Connection();
                }
            }
            DeleteDots();
        }

        private void DeleteDots() 
        {
            dots[0] = null;
            dots[1] = null;
        }

        private void OnUpdate(object sender, EventArgs e) 
        {
            ChangePosition(dots[0].Dot.Connector.X, dots[0].Dot.Connector.Y, dots[1].Dot.Connector.X, dots[1].Dot.Connector.Y);
        }

        private void ChangePosition(double X1, double Y1, double X2, double Y2) 
        {
            Connector.X1 = X1;
            Connector.Y1 = Y1;
            Connector.X2 = X2;
            Connector.Y2 = Y2;
        }

        public DotPlacement ConnectedTo(DotPlacement dot)
        { 
            if (dots[0] == dot) 
            {
                return dots[1];
            }
            else if (dots[1] == dot)
            { 
                return dots[0];
            }
            return null;
        }

        public bool IsConnected 
        { 
            get => dots[0] != null && dots[1] != null;
        }

        public void OnMouseClick(object sender, MouseEventArgs e) 
        { 
            BreakConnection();
        }

        private bool CanConnect(DotPlacement dot1, DotPlacement dot)
        {
            return dot1 != null && dot != null
                && dot1.DotTemplate.IO != dot.DotTemplate.IO
                && dot1.Parent != dot.Parent
                && dot1.DotTemplate.IO != IO.Error && dot.DotTemplate.IO != IO.Error
                && dot1.DotTemplate.Type.IsCompatible(dot.DotTemplate.Type);
        }
    }
}
