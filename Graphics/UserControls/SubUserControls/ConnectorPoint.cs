using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
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
    public class ConnectorPoint : UserControl
    {
        public double X {  get; set; }
        public double Y {  get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private static Point point = new Point(0, 0);

        public static Canvas Canvas 
        { 
            get => MainWindow.mainWindow.mainCanvas;
        }

        public ConnectorPoint()
        {
            this.LayoutUpdated += LayoutUpdate; 
            App.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                LayoutUpdate(null, null);
            }), System.Windows.Threading.DispatcherPriority.Loaded);
        }
        public void LayoutUpdate(object sender, EventArgs e) 
        { 
            if(Canvas.IsAncestorOf(this))
            {
                Point temp = TransformToAncestor(Canvas).Transform(point);
                if (X != temp.X || Y != temp.Y) 
                { 
                    X = temp.X;
                    Y = temp.Y;
                    PropertyChanged?.Invoke(this.Parent, new PropertyChangedEventArgs("Position"));
                }
            }
        }
    }
}
