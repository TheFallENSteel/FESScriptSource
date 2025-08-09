using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using FESScript.CodeWorks.Functions;
using FESScript.Graphics.UserControls;

namespace FESScript.CodeWorks.UserControls
{
    public class UserControlManager
    {
        public Camera Camera { get; private set; }
        private bool MiddleMouseClicked { get; set; }
        private bool IsFullScreen { get; set; }
        private MainWindow _mainWindow;
        public UserControlManager(MainWindow mainWindow, Canvas mainCanvas)
        {
            _mainWindow = mainWindow;
            Camera = new Camera(mainCanvas);
        }
        public void OnKeyDown(Key key) 
        { 
            switch (key) 
            {
                case Key.Left:
                    Camera.CameraUserMove(-1, 0);
                    break;
                case Key.Right:
                    Camera.CameraUserMove(1, 0);
                    break;
                case Key.Up:
                    Camera.CameraUserMove(0, -1);
                    break;
                case Key.Down:
                    Camera.CameraUserMove(0, 1);
                    break;
                case Key.F11:
                    IsFullScreen = !IsFullScreen;
                    if (IsFullScreen) _mainWindow.WindowState = WindowState.Maximized;
                    else _mainWindow.WindowState = WindowState.Normal;
                    break;
            }
            if (key == Key.LeftCtrl || key == Key.RightCtrl) 
            {
                Camera.StartZoom();
            }
        }
        public void OnKeyUp(Key key)
        {
            if (key == Key.LeftCtrl || key == Key.RightCtrl)
            {
                Camera.StopZoom();
            }
        }
        public void OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            Camera.Zoom(e.Delta);
        }

        public void UpdateGlobalPosition()
        {
            Camera.UpdateCamera();
        }
        public void OnMouseDown(MouseButtonEventArgs e)
        {
            if (e.MiddleButton == MouseButtonState.Pressed || e.RightButton == MouseButtonState.Pressed)
            {
                //Mouse.Capture(this);
                Camera.Offset = e.GetPosition(_mainWindow);
                MiddleMouseClicked = true;
            }
        }
        public void OnMouseMove(MouseEventArgs e)
        {
            if (MiddleMouseClicked && (e.MiddleButton == MouseButtonState.Pressed || e.RightButton == MouseButtonState.Pressed))
            {
                Camera.ChangePosition(e, _mainWindow);
            }
            else if (MiddleMouseClicked)
            {
                MiddleMouseClicked = false;
                _mainWindow.ReleaseMouseCapture();
            }
            if (!IMoveable.IsMovable && e.LeftButton == MouseButtonState.Pressed)
            {
                Connection.CurrentConnection.ContinueConnection(e.GetPosition(_mainWindow.mainCanvas));
            }
            else if (!IMoveable.IsMovable)
            {
                Connection.CurrentConnection.FinishConnection(null);
            }
        }
    }
}