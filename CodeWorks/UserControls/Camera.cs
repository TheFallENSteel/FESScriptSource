using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace FESScript.CodeWorks.UserControls
{
    public class Camera(Canvas canvas)
    {
        private const double MaxZoom = 199;
        private const double MinZoom = 1;
        private const double DefaultZoom = 100;
        private const double ZoomDivider = 50;
        private const double CameraMoveSpeed = 1;

        public event EventHandler CameraMoveEvent;
        private Canvas canvas = canvas;

        public Point CameraPosition { get; set; }
        private double _zoom = DefaultZoom;
        private bool CanZoom { get; set; }
        public Point Offset { get; set; }

        public void SetZoom(double value) 
        {
            if (value >= MaxZoom)
            {
                _zoom = MaxZoom;
            }
            else if (value <= MinZoom)
            {
                _zoom = MinZoom;
            }
            else
            {
                _zoom = value;
            }
            Point mousePos = Mouse.GetPosition(canvas);
            canvas.RenderTransformOrigin = new Point(0.5, 0.5);
            canvas.RenderTransform = new ScaleTransform(100 / _zoom, 100 / _zoom, 0, 0);
            UpdateCamera();
        }
        public double GetZoom() 
        {
            return _zoom;
        }
        public void Zoom(int delta) 
        {
            if (CanZoom && delta != 0)
            {
                SetZoom(_zoom - delta / ZoomDivider);
            }
        }
        public void UpdateCamera() 
        { 
            CameraMoveEvent?.Invoke(null, EventArgs.Empty);
        }
        private void CameraMove(double x, double y)
        {
            double posX, posY;
            CameraRelativePos(
                x * CameraMoveSpeed, 
                y * CameraMoveSpeed, 
                out posX, out posY);
            CameraPosition = new Point(posX, posY);
            UpdateCamera();
        }
        public void CameraUserMove(double x, double y)
        {
            double posX, posY;
            CameraRelativePos(x, y, out posX, out posY);
            CameraPosition = new Point(posX, posY);
            UpdateCamera();
        }
        private void CameraRelativePos(double x, double y, out double posX, out double posY)
        {
            posX = CameraPosition.X + x;
            posY = CameraPosition.Y + y;
        }
        public void ChangePosition(MouseEventArgs e, MainWindow window)
        {
            UIElement _window = window as UIElement;
            double pointX = (-e.GetPosition(_window).X) + Offset.X;
            double pointY = (-e.GetPosition(_window).Y) + Offset.Y;
            this.CameraMove(
                pointX * (_zoom / 100), 
                pointY * (_zoom / 100));
            Offset = e.GetPosition(_window);
            UpdateCamera();
        }
        public void StopZoom()
        {
            CanZoom = false;
        }
        public void StartZoom()
        {
            CanZoom = true;
        }
    }
}
