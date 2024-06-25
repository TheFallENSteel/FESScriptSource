using FESScript2.Graphics.UserControls.SubUserControls;
using FESScript2.Graphics.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Input;
using FESScript2.CodeWorks.Functions;

namespace FESScript2.Graphics.UserControls 
{
    public class Connection
    {

        public static Connection CurrentConnection;
        
        Dots[] dots = new Dots[2];

        Line Connector { get; set; }

        public Connection()
        {
            Construct();
        }

        private void Construct(Dots dot1 = null, Dots dot2 = null) 
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
            if(dots[0] != null) Connector.Stroke = ColorsBrushes.TypeToBrush[dots[0].DotType];
            this.Connector.MouseDown += OnMouseClick;
        }

        public Connection(Dots dot1, Dots dot2)
        {
            App.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                MainWindow.mainWindow.mainCanvas.Children.Add(this.Connector);
                OnUpdate(null, null);
            }), System.Windows.Threading.DispatcherPriority.Loaded);
            Construct(dot1, dot2);
            ConnectionCreation();
        }

        public Dots[] Dots 
        { 
            get => this.dots; 
            set => this.dots = value; 
        }

        public void StartConnection(Dots dot)
        {
            BreakConnection();
            MainWindow.mainWindow.mainCanvas.Children.Add(this.Connector);
            CurrentConnection = this;
            IMoveable.IsMovable = false;
            dots[0] = dot;
            Connector.IsHitTestVisible = false;
            Connector.Stroke = ColorsBrushes.TypeToBrush[dot.DotType];
        }

        public void ContinueConnection(Point MousePosition) 
        { 
            ChangePosition(dots[0].Connector.X, dots[0].Connector.Y, MousePosition.X, MousePosition.Y);
        }

        public void FinishConnection(Dots dot)
        {
            IMoveable.IsMovable = true;
            if (dot != null && CurrentConnection.CanConnect(CurrentConnection.dots[0], dot))
            {
                dot.connection.BreakConnection();
                CurrentConnection.dots[1] = dot;
                CurrentConnection.dots[1].connection = CurrentConnection;
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
            foreach (Dots dot in dots) 
            {
                if (dot != null)
                {
                    dot.Connector.PropertyChanged += OnUpdate;
                    dot.connection = this;
                }
            }
            Connector.IsHitTestVisible = true;
            OnUpdate(null, null);
        }

        private void BreakConnection() 
        {
            MainWindow.mainWindow.mainCanvas.Children.Remove(this.Connector);
            foreach (Dots dot in dots)
            {
                if (dot!=null) 
                { 
                    dot.Connector.PropertyChanged -= OnUpdate;
                    dot.connection = new Connection();
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
            ChangePosition(dots[0].Connector.X, dots[0].Connector.Y, dots[1].Connector.X, dots[1].Connector.Y);
        }

        private void ChangePosition(double X1, double Y1, double X2, double Y2) 
        {
            Connector.X1 = X1;
            Connector.Y1 = Y1;
            Connector.X2 = X2;
            Connector.Y2 = Y2;
        }

        public Dots ConnectedTo(Dots dot)
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

        private bool CanConnect(Dots dot1, Dots dot)
        {
            return dot1 != null && dot != null
                && dot1.IO != dot.IO
                && dot1.BlockParent != dot.BlockParent
                && dot1.IO != IO.Error && dot.IO != IO.Error
                && (dot1.DotType != dot.DotType
                || dot1.DotType.IsCompatible(dot.DotType));
        }
    }
}
