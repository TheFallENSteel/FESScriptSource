using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FESScript2.Graphics.UserControls.SubUserControls
{
    /// <summary>
    /// Interaction logic for Dots.xaml
    /// </summary>
    public partial class Dots : UserControlPlus, CodeWorks.IName
    {
        public Dots()
        {
            InitializeComponent();
            //DependencyProperty.Register("X", typeof(float), typeof(Dots), ;
        }

        public Connect connection;

        Type dotType;

        public bool isConditional;

        public Point Position 
        { 
            get 
            {
                Point x = this.TranslatePoint(new Point(0, 0), MainWindow.mainWindow.mainCanvas);
                Matrix transforms = ((Block)((Grid)((Grid)this.Parent).Parent).Parent).RenderTransform.Value;
                Point ellipsePoint = new Point((ellipse.Width / 2) + this.Padding.Left, (ellipse.Height / 2) + this.Padding.Top) * transforms;
                return new Point(ellipsePoint.X + x.X, ellipsePoint.Y + x.Y);
            }
        }

        public new string Name 
        {
            get
            {
                return dotName;
            }
            set 
            {
                dotName = value;
            }
        }

        private string dotName;

        /// <summary>
        /// Sets type of dot and sets color of fill;
        /// </summary>

        public Type DotType
        { 
            get 
            {
                return dotType;
            }
            set 
            {
                dotType = value;
                ellipse.Fill = ColorsBrushes.TypeToBrush[value];
            }
        }
        public Block BlockParent;

        public Dots ConnectedTo 
        { 
            get 
            {
                if (connection != null) 
                { 
                    return connection.ConnectedTo(this);
                }
                return null;
            }
        }

        public IO io;

        /// <summary>
        /// ID of the dot.
        /// </summary>

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

        public void OnParentMove(object sender, EventArgs e) 
        {
            if (connection != null) 
            {
                if (connection.isConnected) 
                {
                    connection.UpdatePosition();
                }
            }
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (connection == null || !connection.isConnected)
            {
                if (Connect.currentConnection.AddDot(this)) 
                {
                    Connect.currentConnection = new Connect();
                }
            }
            else 
            {
                connection.BreakConnection();
                if (Connect.currentConnection.AddDot(this))
                {
                    Connect.currentConnection = new Connect();
                }
            }
        }
    }
}
