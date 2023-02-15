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
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FESScript2.Graphics.SupportWindow
{
    public class BlockViewer : UserControl
    {
        const double animationAntiSpeed = 0.5;
        CodeWorks.BlockCreation.Category category;
        Grid grid = new Grid();
        private ItemsControl itemsControl;
        DoubleAnimation doubleAnimationSecondPanel;
        public ScrollViewer scrollViewer;
        Storyboard storyboardSecondPanel;
        Expander expander;
        public bool isShown;

        public CodeWorks.BlockCreation.Category currentCategory 
        {
            get 
            {
                return category;
            }
            set 
            {
                category = value;
            }
        }

        public BlockViewer(double height, CodeWorks.BlockCreation.Category category, Expander expander)
        {
            this.expander = expander;
            Width = 0;
            isShown = false;
            App.CategoryChanged += OnCategoryChange;
            currentCategory = category;
            itemsControl = new ItemsControl();
            this.Content = grid;
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            //grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(15)});
            //grid.ColumnDefinitions.Add(new ColumnDefinition());
            GridSplitter gridSplitter = new GridSplitter();
            gridSplitter.ShowsPreview = true;
            gridSplitter.Background = Brushes.Aqua;
            Grid.SetColumn(gridSplitter, 1);
            grid.Children.Add(gridSplitter);
            itemsControl.Background = CustomBrushes.OrangeMenu2;
            itemsControl.IsTabStop = false;
            itemsControl.Width = double.NaN;
            itemsControl.HorizontalContentAlignment = HorizontalAlignment.Left;
            itemsControl.VerticalContentAlignment = VerticalAlignment.Top;
            itemsControl.HorizontalAlignment = HorizontalAlignment.Stretch;
            itemsControl.VerticalAlignment = VerticalAlignment.Stretch;
            scrollViewer = new ScrollViewer();
            scrollViewer.HorizontalAlignment = HorizontalAlignment.Left;
            scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
            scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
            scrollViewer.Content = itemsControl;
            grid.Children.Add(scrollViewer);

            doubleAnimationSecondPanel = new DoubleAnimation();
            doubleAnimationSecondPanel.AccelerationRatio = 0.5;
            storyboardSecondPanel = new Storyboard();
            Storyboard.SetTargetProperty(doubleAnimationSecondPanel, new PropertyPath(BlockViewer.WidthProperty));
            storyboardSecondPanel.RepeatBehavior = new RepeatBehavior(1);
            storyboardSecondPanel.Children.Add(doubleAnimationSecondPanel);
            itemsControl.HorizontalAlignment = HorizontalAlignment.Center;
            itemsControl.Padding = new Thickness(10);
        }
        private void ShowCategory() 
        {
            expander.UpdateHeight();
            if (isShown && itemsControl.ItemsSource != currentCategory.blocks)
            {
                Hide(true);
            }
            else if (isShown) 
            {
                Hide();
            }
            else 
            {
                Show();
            }
            isShown = !isShown;
        }

        public void Hide(bool showAfter = false) 
        {
            storyboardSecondPanel.Stop();
            if (showAfter) 
            {
                storyboardSecondPanel.Completed += OnCompletedShow;
            }
            doubleAnimationSecondPanel.From = this.ActualWidth; // itemsControl.ActualWidth;
            doubleAnimationSecondPanel.To = 0;
            doubleAnimationSecondPanel.Duration = new Duration(TimeSpan.FromSeconds(animationAntiSpeed));
            //canvas.Children.Add(expanderWindow);
            storyboardSecondPanel.Begin(this);
        }

        public void Show()
        {
            storyboardSecondPanel.Stop();
            itemsControl.ItemsSource = currentCategory.blocks;
            //this.Width = double.NaN;
            itemsControl.UpdateLayout();
            doubleAnimationSecondPanel.From = this.ActualWidth;
            doubleAnimationSecondPanel.To = itemsControl.ActualWidth;
            doubleAnimationSecondPanel.Duration = new Duration(TimeSpan.FromSeconds((animationAntiSpeed / itemsControl.ActualWidth) * MathF.Abs((float)(itemsControl.ActualWidth - this.ActualWidth))));
            //canvas.Children.Add(expanderWindow);
            storyboardSecondPanel.Begin(this);
        }

        private void OnCategoryChange(CodeWorks.BlockCreation.Category category) 
        {
            currentCategory = category;
            ShowCategory();
        }

        private void OnCompletedShow(object sender, EventArgs args) 
        {
            Show();
            isShown = !isShown;
            storyboardSecondPanel.Completed -= OnCompletedShow;
        }
    }
}
