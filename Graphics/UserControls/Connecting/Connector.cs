using System.Collections.Generic;
using FESScript2.Graphics.UserControls.SubUserControls;
using System;
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
using System.Windows.Ink;
using Microsoft.VisualBasic;

namespace FESScript2.Graphics.UserControls
{
    public class Connect
    {
        public static Connect currentConnection = new Connect();
        Dots dot1;
        Dots dot2;
        Line connectionLine;
        public bool isConnected = false;
        public Connect() 
        {

        }

        private void RemoveFromConnection(Dots dot) 
        { 
            if (dot1 == dot) 
            {
                dot1 = dot2;
                dot2 = null;
            }
            else if (dot2 == dot) 
            {
                dot2 = null;
            }
        }


        public void BreakConnection(object sender = null, MouseButtonEventArgs e = null)
        {
            dot1.connection = null;
            dot2.connection = null;
            dot1 = null;
            dot2 = null;
            MainWindow.mainWindow.mainCanvas.Children.Remove(connectionLine);
            connectionLine = null;
            isConnected = false;
        }

        public void UpdatePosition() 
        {
            Point dot1Pos = dot1.Position;
            Point dot2Pos = dot2.Position;
            connectionLine.X1 = dot1Pos.X;
            connectionLine.Y1 = dot1Pos.Y;
            connectionLine.X2 = dot2Pos.X;
            connectionLine.Y2 = dot2Pos.Y;
            if (dot1.DotType != dot2.DotType) 
            { 
                if (dot2.Position.X <= dot1.Position.X)
                {
                    connectionLine.Stroke = new LinearGradientBrush(SubUserControls.ColorsBrushes.TypeToColor[dot2.DotType], SubUserControls.ColorsBrushes.TypeToColor[dot1.DotType], Math.Atan((dot2.Position.Y - dot1.Position.Y) / (dot2.Position.X - dot1.Position.X)));
                }
                else
                {
                    connectionLine.Stroke = new LinearGradientBrush(SubUserControls.ColorsBrushes.TypeToColor[dot1.DotType], SubUserControls.ColorsBrushes.TypeToColor[dot2.DotType], Math.PI - Math.Atan((dot2.Position.Y - dot1.Position.Y) / (dot2.Position.X - dot1.Position.X)));
                }
            }
            /*if (dot2.Position.X > dot1.Position.X) 
            {
                connectionLine.Stroke = new RadialGradientBrush() {GradientStops = new GradientStopCollection() { new GradientStop(SubUserControls.Colors.TypeToColor[dot1.DotType], 0), new GradientStop(SubUserControls.Colors.TypeToColor[dot2.DotType], 1)} };
                //connectionLine.Stroke = new LinearGradientBrush(SubUserControls.Colors.TypeToColor[dot1.DotType], SubUserControls.Colors.TypeToColor[dot2.DotType], Math.PI + Math.Atan((dot2.Position.Y - dot1.Position.Y)/ (dot2.Position.X - dot1.Position.X)));
            }
            else
            {
                connectionLine.Stroke = new RadialGradientBrush() { GradientStops = new GradientStopCollection() { new GradientStop(SubUserControls.Colors.TypeToColor[dot1.DotType], 0), new GradientStop(SubUserControls.Colors.TypeToColor[dot2.DotType], 1) } };
                //connectionLine.Stroke = new RadialGradientBrush(SubUserControls.Colors.TypeToColor[dot1.DotType], SubUserControls.Colors.TypeToColor[dot2.DotType]) { RadiusX = dot2.Position.X - dot1.Position.X, RadiusY = dot2.Position.Y - dot1.Position.Y, Center = new Point(0.5, 0.5), GradientOrigin = new Point(0.5, 0.5) };
                //connectionLine.Stroke = new LinearGradientBrush(SubUserControls.Colors.TypeToColor[dot1.DotType], SubUserControls.Colors.TypeToColor[dot2.DotType], Math.Atan((dot2.Position.Y - dot1.Position.Y) / (dot2.Position.X - dot1.Position.X)));
            }*/
        }

        private void MakeConnection() 
        {
            dot1.connection = this;
            dot2.connection = this;
            isConnected = true;
            connectionLine = new Line();
            connectionLine.StrokeStartLineCap = PenLineCap.Round;
            connectionLine.StrokeEndLineCap = PenLineCap.Round;
            connectionLine.Stroke = SubUserControls.ColorsBrushes.TypeToBrush[dot1.DotType];
            UpdatePosition();

            connectionLine.MouseDown += BreakConnection;
            connectionLine.StrokeThickness = 5;
            Canvas.SetZIndex(connectionLine, int.MaxValue);
            MainWindow.mainWindow.mainCanvas.Children.Add(connectionLine);
        }

        public Dots ConnectedTo(Dots dot) 
        { 
            if (this.dot1 == dot) 
            {
                return dot2;
            }
            else if (this.dot2 == dot)
            {
                return dot1;
            }
            return null;
        }

        /// <returns>Returns <see cref="true"/> if connection was completed.</returns>

        public bool AddDot(Dots dot) 
        {
            if (dot1 == null) 
            {
                this.dot1 = dot;
                return false;
            }
            else if (dot2 == null && CanConnect(dot1, dot) && dot1.connection == null) 
            {
                if (dot.connection != null && dot.connection.isConnected) 
                {
                    dot.connection.BreakConnection();
                }
                dot2 = dot;
                MakeConnection();
                return true;
            }
            else 
            {
                RemoveFromConnection(dot1);
                AddDot(dot);
                return true;
            }
        }

        private bool CanConnect(Dots dot1, Dots dot) 
        {
            return dot1.IO != dot.IO
                && dot1.IO != IO.Error && dot.IO != IO.Error
                && (dot1.DotType != dot.DotType
                || dot1.DotType.IsCompatible(dot.DotType));
        }
    }
}
