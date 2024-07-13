using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FESScript.CodeWorks.BlockCreation.Blocks;
using FESScript.CodeWorks.BlockCreation.Blocks.Templates;
using FESScript.CodeWorks.BlockCreation.Blocks.Placements;
using FESScript.CodeWorks.BlockCreation.Interfaces;
using FESScript.CodeWorks.BlockCreation.Blocks.UserElements;

namespace FESScript.CodeWorks.BlockCreation
{
    public class Category : IFindable
    {
        public int ID { get; set; }
        public static List<Category> categories = new List<Category>();
        public List<BlockPlacement> blockPlacements = new List<BlockPlacement>();
        public string Name { get; set; }

        public IFindable Parent { get => null; }

        public Category(string name)
        {
            Name = name; 
        }
       
        public static void AddOrCreate(string catName, ref BlockTemplate blockData)
        {
            Category desiredCat = categories.Find((cat) => cat.Name == catName); //HahaName
            if (desiredCat == null) 
            {
                desiredCat = new Category(catName);
                categories.Add(desiredCat);
            }
            UserControl userControl = new UserControl();
            BlockPlacement block = new BlockPlacement(blockData, desiredCat, null);
            block.Block.HorizontalAlignment = HorizontalAlignment.Center;
            block.Block.Margin = new Thickness(0, 15, 0, 0);
            block.Block.IsEnabled = false;
            userControl.Content = block;
            userControl.MouseDown += OnBlockClick;
        }

        private static void OnBlockClick(object sender, MouseButtonEventArgs args) 
        {
            BlockPlacement block = new BlockPlacement(((Block)((UserControl)sender).Content).BlockPlacement.BlockTemplate, null, App.Window.mainCanvas);
            block.Move(
                App.Window.MainMenu.expander.canvas1.ActualWidth + App.Window.MainMenu.expander.blockViewer.ActualWidth, 
                ((UserControl)sender).TransformToVisual(App.Window.mainCanvas).Transform(new Point(0, 0)).Y + 15, false);
            //Canvas.SetLeft(block, MainWindow.mainMenu.expander.canvas1.ActualWidth + MainWindow.mainMenu.expander.blockViewer.ActualWidth);
            //Canvas.SetTop(block, ((UserControl)sender).TransformToVisual(MainWindow.mainWindow.mainCanvas).Transform(new Point(0,0)).Y + 15);
        }

        public IFindable FindChild(int ID)
        {
            return blockPlacements.Find((blockPlacement) => blockPlacement.ID == ID);
        }
    }
}
