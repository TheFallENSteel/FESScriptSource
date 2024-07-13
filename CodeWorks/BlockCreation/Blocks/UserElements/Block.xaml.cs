using FESScript.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
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
using FESScript.CodeWorks.Functions;
using FESScript.CodeWorks.BlockCreation.Interfaces;
using FESScript.CodeWorks.BlockCreation.Blocks.Placements;
using System.ComponentModel;
using System.Reflection;
using System.Collections.Specialized;
using System.IO;
using FESScript.CodeWorks.BlockCreation.Blocks.UserElements.Commands;

namespace FESScript.CodeWorks.BlockCreation.Blocks.UserElements
{
    /// <summary>
    /// Interaction logic for Block.xaml
    /// </summary>
    public partial class Block : UserControl, INotifyPropertyChanged, INotifyCollectionChanged
    {
        public BlockPlacement BlockPlacement { get; init; }

        public Block(BlockPlacement blockPlacement)
        {
            this.BlockPlacement = blockPlacement;
            blockPlacement.Block = this;
            blockPlacement.EventSubscribe();
            this.DataContext = BlockPlacement;
            InitializeComponent();
            BlockPlacement.Show();
            this.BlockPlacement.PropertyChanged += Change;
            this.MouseUp += this.MouseButtonUp;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public void Remove() 
        {
            this.BlockPlacement.PropertyChanged -= Change;
        }

        private void Change(object sender, PropertyChangedEventArgs args) 
        {
            PropertyChanged?.Invoke(sender, args);
            CollectionChanged?.Invoke(sender, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        public void UpdatePanels() 
        { 
            //Test
            InDotsPanel.GetBindingExpression(ItemsControl.ItemsSourceProperty).UpdateTarget();
            OutDotsPanel.GetBindingExpression(ItemsControl.ItemsSourceProperty).UpdateTarget();
            ContentPanel.GetBindingExpression(ItemsControl.ItemsSourceProperty).UpdateTarget();
        }

        private void UserControl_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            //this.ContextMenu.IsOpen = !this.ContextMenu.IsOpen;
        }

        private void MouseButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (e.RightButton == MouseButtonState.Pressed) this.ContextMenu.IsOpen = !this.ContextMenu.IsOpen;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            switch ((sender as MenuItem).Header) 
            {
                case "Delete":
                    BlockCommands.Delete(BlockPlacement);
                    break;
                case "Duplicate":
                    BlockCommands.Duplicate(BlockPlacement);
                    break;
                case "Reset":
                    BlockCommands.Reset(BlockPlacement);
                    break;
            }
        }
    }
}
