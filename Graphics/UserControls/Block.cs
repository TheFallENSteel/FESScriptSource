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

namespace FESScript2.Graphics.UserControls
{
    /// <summary>
    /// Struct used to reconstruct block.
    /// </summary>

    public class BlockType 
    {

        public BlockType(int id, string category, string name = "", SubUserControls.Type type = SubUserControls.Type.Error) 
        {
            this.Category = category;
            this.id = id;
            this.name = name;
            this.type = type;
            dots = new List<DotsType>();
            singleActionOutput = !IsMoreThanOneActionOutputDot();
            contents = new List<ContentsType>();
            global.Add(this);
        }

        public bool isFake;

        public bool createFunction = true;

        public string fakeString;

        public string Category;

        public bool IsMoreThanOneActionOutputDot() 
        {
            int dotCount = 0;
            foreach (DotsType dot in dots)
            {
                if ((dot.dotType == SubUserControls.Type.Action || dot.dotType == SubUserControls.Type.SubAction) && dot.io == IO.Output)
                {
                    dotCount++;
                }
            }
            if (dotCount >= 2)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Determines whether there is more than one Action Output Dot.
        /// </summary>

        public bool singleActionOutput;

        public static BlockType Find(int id) 
        { 
            for (int i = 0; i < global.Count; i++) 
            {
                if (global[i].id == id) 
                {
                    return global[i];
                }
            }
            return null;
        }

        /// <summary>
        /// Global BlockType register.
        /// </summary>

        public static List<BlockType> global = new List<BlockType>();

        /// <summary>
        /// Unique identifier of block type.
        /// </summary>
        public int id;

        /// <summary>
        /// Name of the block type.
        /// </summary>

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        string name;

        /// <summary>
        /// Type of the block.
        /// </summary>

        public FESScript2.Graphics.UserControls.SubUserControls.Type type;

        /// <summary>
        /// Struct that stores data needed for dot reconstruction.
        /// </summary>
        
        public List<DotsType> dots;

        /// <summary>
        /// Struct that stores data needed for contents of block reconstruction.
        /// </summary>

        public List<ContentsType> contents;
    }

    /// <summary>
    /// The visual block.
    /// </summary>

    public class Block : UserControlPlus, CodeWorks.Functions.IMoveable, CodeWorks.IName
    {
        public event EventHandler OnMove;
        /// <summary>
        /// Determines whether events should be subscribed.
        /// </summary>
        private bool subscribeToEvents;

        private void OnKeyDown(object sender, KeyEventArgs e) 
        { 
            if ((e.Key == Key.D || e.Key == Key.Delete) && isClicked) 
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
            if (contentName != null) 
            { 
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
            return null;
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
            if (subscribeToEvents)
            {
                MainWindow.mainWindow.CameraMoveEvent += ((CodeWorks.Functions.IMoveable)this).Redraw;
            }
        }

        public new string Name 
        { 
            get 
            {
                return name;
            }
            set 
            {
                name = value;
            }
        }

        public static void DestroyAll()
        {
            MainWindow.mainWindow.mainCanvas.Children.Clear();
            blocks.Clear();
        }

        string name;

        public Point Position
        { 
            get 
            {
                return position;
            }
            set 
            {
                position = value;
            }
        }

        private Point position;

        /// <summary>
        /// Determines if is clicked.
        /// </summary>

        public bool IsClicked
        {
            get 
            {
                return isClicked;
            }
            set 
            {
                isClicked = value;
            }
        }
        private bool isClicked;

        /// <summary>
        /// Determines last mouse position.
        /// </summary>

        public Point Offset
        {
            get 
            {
                return offSet;
            }
            set 
            {
                offSet = value;
            }
        }
        private Point offSet;

        /// <summary>
        /// Global list of all blocks created.
        /// </summary>

        public static List<Block> blocks = new List<Block>();

        public Grid grid;

        /// <summary>
        /// Gets unique id of every Block.
        /// </summary>

        public int Id 
        { 
            get 
            {
                return blocks.IndexOf(this);
            }
        }

        public List<Dots> dots = new List<Dots>();

        public List<Dots> InputNonActionDots = new List<Dots>();
        public List<Dots> ActionoutputDots = new List<Dots>();
        public List<IContents> contentsInteractive = new List<IContents>();

        public BlockType blockType;

        /// <summary>
        /// Block type id.
        /// </summary>

        public int BlockTypeId;

        /// <summary>
        /// Determines whether the block should be shown.
        /// </summary>

        public bool isShown;

        /// <summary>
        /// Shows block.
        /// </summary>

        public void Show() 
        {
            if(!isShown) 
            {
                blocks.Add(this);
                MainWindow.mainWindow.mainCanvas.Children.Add(this);
                this.isShown = true;
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

        /// <summary>
        /// Destroys block.
        /// </summary>

        public void Delete()
        {
            MainWindow.mainWindow.mainCanvas.Children.Clear();
            blocks.Remove(this);
            this.isShown = false;
        }

        public void DeleteBlock()
        {
            MainWindow.mainWindow.mainCanvas.Children.Remove(this);
            blocks.Remove(this);
            this.isShown = false;
        }

        /// <summary>
        /// Whether it should be shown
        /// </summary>
        /// <param name="show">Determines whether Show() should be called.</param>

        public Block(bool show = false, bool subscribeToEvents = true) : base()
        {
            this.subscribeToEvents = subscribeToEvents;
            ((CodeWorks.Functions.IMoveable)this).EventSubscribe();
            Focusable = true;
            Loaded += (sender,e) => Keyboard.Focus(this);
            KeyDown += OnKeyDown;
            MouseDown += ((CodeWorks.Functions.IMoveable)this).MouseDown;
            MouseMove += ((CodeWorks.Functions.IMoveable)this).MouseMove;
            Canvas.SetLeft(this, 0);
            Canvas.SetTop(this, 0);
            MinHeight = 40;
            grid = new Grid();
            this.Content = grid;
            this.SizeChanged += Resize;
            if (show) 
            {
                Show();
            }
        }
        private void Resize(object sender, SizeChangedEventArgs e) 
        {
            OnMove?.Invoke(null, null);
        }

        /// <summary>
        /// Activates when mouse moves over object.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Arguments of mouse.</param>
    }
}
