using FESScript.CodeWorks.BlockCreation.Interfaces;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using FESScript.CodeWorks.Functions;
using System.Windows.Input;
using System.Windows;
using FESScript.CodeWorks.BlockCreation.Blocks.Templates;
using FESScript.CodeWorks.BlockCreation.Blocks.UserElements;
using System.Linq;
using System.ComponentModel;
using System.Collections.Specialized;

namespace FESScript.CodeWorks.BlockCreation.Blocks.Placements
{
    public class BlockPlacement : CanvasItem, IFindable, IInfo, IRemovable, INotifyPropertyChanged, INotifyCollectionChanged, ICloneable
    {
        public static List<BlockPlacement> blockPlacements = new List<BlockPlacement>();

        public event PropertyChangedEventHandler PropertyChanged;
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public BlockTemplate BlockTemplate { get; set; }

        public int ID { get; set; }
        public IFindable Parent { get; set; }
        public string Name { get => BlockTemplate.Name; }
        public string Description { get => BlockTemplate.Description; }

        public Block Block { get; set; }

        public IFindable FindChild(int ID) => BlockTemplate.FindChild(ID);
        public DotPlacement FindDot(int ID) => DotPlacements.Find((dot) => dot.ID == ID);
        public ContentPlacement FindContent(int ID) => ContentPlacements.Find((content) => content.ID == ID);

        public List<DotPlacement> DotPlacements { get; set; } = new List<DotPlacement>();
        public List<ContentPlacement> ContentPlacements { get; set; } = new List<ContentPlacement> { };
        public override UserControl Item { get => Block; }

        public BlockPlacement(BlockTemplate blockData, IFindable parent, Panel container) : this(blockData, parent, container, new Point(0, 0)) { }
        public BlockPlacement(BlockTemplate blockData, IFindable parent, Panel container, Point point) : base(container, point)
        {
            BlockTemplate = blockData;
            Parent = parent;
            UpdateTemplate();
            (this as IFindable).Register();
            Block = new Block(this);
            (this as CanvasItem).DrawPosition();
        }

        public void UpdateTemplate(object sender = null, EventArgs args = null) 
        { 
            ContentPlacements = BlockTemplate.Contents.Select(contentTemplate => new ContentPlacement(contentTemplate, this)).ToList();
            DotPlacements = BlockTemplate.Dots.Select(dotTemplate => new DotPlacement(dotTemplate, this)).ToList();
            PropertyChanged?.Invoke(sender, new PropertyChangedEventArgs("SomeProperty"));
            CollectionChanged?.Invoke(sender, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            Block?.UpdatePanels();
        }

        public override void EventSubscribe()
        {
            base.EventSubscribe();
            Block.KeyDown += OnKeyDown;
            BlockTemplate.PropertyChanged += UpdateTemplate;
            Block.Loaded += (sender, e) => Keyboard.Focus(Block);
            blockPlacements.Add(this);
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key == Key.D || e.Key == Key.Delete) && IsClicked)
            {
                Remove();
            }
        }

        public void Remove()
        {
            Hide();

            BlockTemplate.PropertyChanged -= UpdateTemplate;
            Block.KeyDown -= OnKeyDown;
            Block.MouseDown -= ((IMoveable)this).MouseDown;
            Block.MouseMove -= ((IMoveable)this).MouseMove;
            Block.SizeChanged -= (_, _) => OnMove?.Invoke(null, null);
            Block.Loaded -= (sender, e) => Keyboard.Focus(Block);
            Block.Focusable = false;
            DotPlacements.ForEach(dotPlacement => dotPlacement.Remove());
            ContentPlacements.ForEach(dotPlacement => dotPlacement.Remove());
            Block.Remove();
            (this as IFindable).UnRegister();
            BlockTemplate = null;
        }

        public static void RemoveAll()
        {
            blockPlacements.ForEach((block) => block.Remove());
            blockPlacements.Clear();
        }

        public object Clone()
        {
            return new BlockPlacement(this.BlockTemplate, this.Parent, this.Container, new Point(Position.X + 10, Position.Y + 10));
        }
    }
}
