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
        }

        public Connect connection;

        Type dotType;

        public bool isConditional;

        public Point Position 
        { 
            get 
            {
                Point point = this.TranslatePoint(new Point(0, 0), MainWindow.mainWindow.mainCanvas);
                Block block = ((Block)((Grid)((Grid)this.Parent).Parent).Parent);
                Matrix transforms = block.RenderTransform.Value;

                Point ellipsePoint = new Point((ellipse.Width / 2) + this.Padding.Left, (ellipse.Height / 2) + this.Padding.Top) * transforms;
                return new Point(ellipsePoint.X + point.X, ellipsePoint.Y + point.Y);
            }
        }

        public new string Name { get; set; }

        /// <summary>
        /// Sets type of dot and sets color of fill;
        /// </summary>

        public Type DotType
        { 
            get => dotType;
            set 
            {
                dotType = value;
                ellipse.Fill = ColorsBrushes.TypeToBrush[value];
            }
        }

        public Block BlockParent;

        public Dots ConnectedTo { get => connection == null ? null : connection.ConnectedTo(this); }

        public IO IO;

        public int ID { get; set; }

        public void OnParentMove(object sender, EventArgs e) 
        {
            if (connection != null && connection.isConnected) connection.UpdatePosition();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (connection != null && connection.isConnected) connection.BreakConnection();
            if (Connect.currentConnection.AddDot(this)) Connect.currentConnection = new Connect(); //Error creates connection again
        }
    }
}
