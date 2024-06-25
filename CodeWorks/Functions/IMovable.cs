using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using FESScript2.Graphics.UserControls;
using System.Windows.Controls;

namespace FESScript2.CodeWorks.Functions
{

    /// <summary>
    /// Use only for things inheriting from <see cref="System.Windows.UIElement"/>.
    /// </summary>

    public interface IMoveable
    {

        /// <summary>
        /// Sets global position of object.
        /// </summary>

        public Point Position
        {
            get;
            set;
        }

        /// <summary>
        /// Determines whether the device is clicked.
        /// </summary>

        public bool IsClicked
        {
            get;
            set;
        }

        public static bool IsMovable = true;

        /// <summary>
        /// Offset of mouse. Offset must not return <see cref="null"/>.
        /// </summary>

        public Point Offset
        {
            get;
            set;
        }

        public static IMoveable lastSelected;

        /// <summary>
        /// Moves object.
        /// </summary>
        /// <param name="x">Move on X axis.</param>
        /// <param name="y">Move on Y axis.</param>
        public void Move(double x, double y, bool relative = true);

        /// <summary>
        /// Detects mouse move over object.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        public void MouseMove(object sender, MouseEventArgs e)
        {
            if (IsClicked && IsMovable && e.LeftButton == MouseButtonState.Pressed)
            {
                double pointX = e.GetPosition((UIElement)this).X - Offset.X;
                double pointY = e.GetPosition((UIElement)this).Y - Offset.Y;
                this.Move(pointX, pointY);
                lastSelected = this;
            }
            else if (IsClicked) 
            {
                IsClicked = false;
                ((UIElement)this).ReleaseMouseCapture();
            }
        }

        /// <summary>
        /// Detects mouse press on object.
        /// </summary>

        public void MouseDown(object sender, MouseEventArgs e)
        {
            Keyboard.Focus((UIElement)this);
            Mouse.Capture((UIElement)this);
            Offset = e.GetPosition((UIElement)this);
            IsClicked = true;
        }



        public void Redraw(object sender, EventArgs e) 
        {
            DrawPosition();
        }

        /// <summary>
        /// Should draw position of object.
        /// </summary>

        public void DrawPosition();

        /// <summary>
        /// Must be override to work right.
        /// CameraMoveEvent += Redraw;
        /// </summary>

        public void EventSubscribe();
    }
}
