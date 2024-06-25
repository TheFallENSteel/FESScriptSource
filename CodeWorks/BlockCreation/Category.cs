using System;
using System.Collections.Generic;
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

namespace FESScript2.CodeWorks.BlockCreation
{
    public class Category
    {
        public static List<Category> categories = new List<Category>();
        public Category(string name)
        {
            Name = name; 
        }
        public List<UserControl> blocks = new List<UserControl>();
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
       
        public static void AddOrCreate(string catName, ref Graphics.UserControls.BlockType blocktype)
        {
            Category desiredCat = categories.Find((cat) => cat.Name == catName); //HahaName
            if (desiredCat == null) 
            {
                desiredCat = new Category(catName);
                categories.Add(desiredCat);
            }
            UserControl userControl = new UserControl();
            Graphics.UserControls.Block block;
            BlockRecreation.RecreateBlock(blocktype, out block, false, false);
            block.HorizontalAlignment = HorizontalAlignment.Center;
            block.Margin = new Thickness(0, 15, 0, 0);
            block.IsEnabled = false;
            userControl.Content = block;
            userControl.MouseDown += OnBlockClick;
            
            
            desiredCat.blocks.Add(userControl);
        }

        private static void OnBlockClick(object sender, MouseButtonEventArgs args) 
        {
            Graphics.UserControls.Block block = new Graphics.UserControls.Block(false, true);
            BlockRecreation.RecreateBlock(((Graphics.UserControls.Block)((UserControl)sender).Content).blockType, out block);
            block.Move(MainWindow.mainMenu.expander.canvas1.ActualWidth + MainWindow.mainMenu.expander.blockViewer.ActualWidth, ((UserControl)sender).TransformToVisual(MainWindow.mainWindow.mainCanvas).Transform(new Point(0, 0)).Y + 15, false);
            //Canvas.SetLeft(block, MainWindow.mainMenu.expander.canvas1.ActualWidth + MainWindow.mainMenu.expander.blockViewer.ActualWidth);
            //Canvas.SetTop(block, ((UserControl)sender).TransformToVisual(MainWindow.mainWindow.mainCanvas).Transform(new Point(0,0)).Y + 15);
        }
    }
}
