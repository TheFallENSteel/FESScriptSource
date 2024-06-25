using System.Collections.Generic;
using FESScript2.Graphics.UserControls.SubUserControls;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using FESScript2.CodeWorks.Functions;

namespace FESScript2.Graphics.UserControls
{
    /// <summary>
    /// The visual block.
    /// </summary>

    public class Block : UserControlPlus, CodeWorks.Functions.IMoveable, CodeWorks.IName
    {
        public static List<Block> blocks = new List<Block>();

        public event EventHandler OnMove;

        public new string Name { get; set; }
        public Point Position { get; set; }
        public bool IsClicked { get; set; }
        public Point Offset { get; set; }
        public int ID { get => blocks.IndexOf(this); }
        public bool IsShown { get; set; }
        public BlockType blockType {  get; set; }
        public int BlockTypeId { get; set; }

        /// <summary>
        /// Global list of all blocks created.
        /// </summary>

        public Grid grid;
        public List<Dots> dots = new List<Dots>();
        public List<Dots> InputNonActionDots = new List<Dots>();
        public List<Dots> ActionoutputDots = new List<Dots>();
        public List<IContents> contentsInteractive = new List<IContents>();

        private void OnKeyDown(object sender, KeyEventArgs e) 
        { 
            if ((e.Key == Key.D || e.Key == Key.Delete) && IsClicked) 
            {
                DeleteBlock();
            }
            /*if ((e.Key == Key.Z) && isClicked)
            {
                RenderTransform = new ScaleTransform(5, 5);
            }*/
        }

        public string GetValueOfRelatives(string contentName) 
        {
            if (contentName == null) return null;

            string[] parts = contentName.Split(' ');
            string returnValue = "";
            for (int i = 0; i < parts.Length; i++) 
            {
                if (parts[i][0] == '$') 
                {
                    returnValue += $" {GetValueOfName(parts[i].Substring(1))}";
                }
                else 
                {
                    returnValue += $" {parts[i]}";
                }
            }
            return returnValue;
        }
        private string GetValueOfName(string contentName) 
        { 
            for (int i = 0; i < contentsInteractive.Count; i++) 
            {
                if (contentsInteractive[i].Name == contentName)
                {
                    return contentsInteractive[i].Text;
                }
            }
            for (int i = 0; i < this.dots.Count; i++) 
            {
                if (this.dots[i].Name == contentName)
                {
                    return @$"{this.dots[i].ConnectedTo.BlockParent.Name}.{this.dots[i].ConnectedTo.Name}";
                }
            }
            return null;
        }

        public virtual void EventSubscribe() 
        {
            KeyDown += OnKeyDown;
            MouseDown += ((IMoveable)this).MouseDown;
            MouseMove += ((IMoveable)this).MouseMove;
            this.SizeChanged += (object _, SizeChangedEventArgs _) => OnMove?.Invoke(null, null);
            Focusable = true;
            Loaded += (sender, e) => Keyboard.Focus(this);

            MainWindow.mainWindow.CameraMoveEvent += ((IMoveable)this).Redraw;
        }

        public static void DestroyAll()
        {
            MainWindow.mainWindow.mainCanvas.Children.Clear();
            blocks.Clear();
        }

        public Dots FindDot(int id) 
        { 
            return dots.Find((Dots dot) => dot.ID == id);
        }

        public IContents FindContent(int id)
        {
            return contentsInteractive.Find((IContents content) => content.ID == id);
        }

        public UserControlPlus FindElement(int id) 
        {
            Dots dot = FindDot(id);
            if (dot != null) return dot;

            IContents content = FindContent(id);
            if (content != null) return (UserControlPlus)content;

            return null;
        }

        /// <summary>
        /// Shows block.
        /// </summary>

        public void Show() 
        {
            if(!IsShown) 
            {
                blocks.Add(this);
                MainWindow.mainWindow.mainCanvas.Children.Add(this);
                this.IsShown = true;
            }
        }

        /// <summary>
        /// It sets position of block;
        /// </summary>
        /// <param name="x">X position.</param>
        /// <param name="y">Y position</param>

        public void Move(double x, double y, bool relativeToScreen = true) 
        {
            if (relativeToScreen) 
            { 
                this.Position = new Point(Position.X + x, Position.Y + y);
            }
            else 
            {
                this.Position = new Point(x + MainWindow.mainWindow.CameraPosition.X, y + MainWindow.mainWindow.CameraPosition.Y);
            }
            DrawPosition();
        }

        public void DrawPosition()
        {
            Canvas.SetLeft(this, this.Position.X - MainWindow.mainWindow.CameraPosition.X);
            Canvas.SetTop(this, this.Position.Y - MainWindow.mainWindow.CameraPosition.Y);
            OnMove?.Invoke(null, null);
        }

        public void DeleteBlock()
        {
            MainWindow.mainWindow.mainCanvas.Children.Remove(this);
            blocks.Remove(this);
            this.IsShown = false;
        }

        public Block(bool show, bool subscribeToEvents) : base()
        {
            if (subscribeToEvents) EventSubscribe();
            
            Canvas.SetLeft(this, 0);
            Canvas.SetTop(this, 0);
            MinHeight = 40;
            grid = new Grid();
            this.Content = grid;
            if (show) Show();
        }
    }
}
