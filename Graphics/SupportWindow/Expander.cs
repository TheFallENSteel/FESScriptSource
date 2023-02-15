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
    public class Expander : UserControl
    {
        bool isOpened;
        const double animationAntiSpeed = 0.75;
        Grid grid = new Grid();
        public Canvas canvas1;
        Grid innerGrid = new Grid();
        ExpanderWindow expanderWindow;
        public BlockViewer blockViewer;
        double canvasHeight;
        DoubleAnimation doubleAnimationFirstPanel;

        Storyboard storyboardFirstPanel;

        public Expander()
        {
            Rectangle background = new Rectangle() { Fill = CustomBrushes.LeftBar };
            grid.Children.Add(background);
            Label labelText = new Label() { FontFamily = new FontFamily("Arial"), Content = "Blocks", HorizontalAlignment = HorizontalAlignment.Center };
            Label labelPlus = new Label() { FontFamily = new FontFamily("Arial"), Content = "+", HorizontalAlignment = HorizontalAlignment.Right};
            grid.Children.Add(labelText);
            grid.Children.Add(labelPlus);
            grid.MouseDown += OnMouseDown;
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(25)});
            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(150) });          
            this.Content = grid;
            Dispatcher.BeginInvoke(new Action(() =>
            {
                MainWindow.mainWindow.SizeChanged += OnWindowResize;
                canvasHeight = MainWindow.mainWindow.mainCanvas.ActualHeight;
                blockViewer.scrollViewer.Height = MainWindow.mainWindow.mainCanvas.ActualHeight;
                blockViewer.scrollViewer.Width = double.NaN;
                blockViewer.scrollViewer.VerticalAlignment = VerticalAlignment.Top;
            }), System.Windows.Threading.DispatcherPriority.Loaded);
            
            //First Panel

            doubleAnimationFirstPanel = new DoubleAnimation();
            doubleAnimationFirstPanel.AccelerationRatio = 0.5;
            storyboardFirstPanel = new Storyboard();
            Storyboard.SetTargetProperty(doubleAnimationFirstPanel, new PropertyPath(ExpanderWindow.HeightProperty));
            storyboardFirstPanel.RepeatBehavior = new RepeatBehavior(1);
            storyboardFirstPanel.Children.Add(doubleAnimationFirstPanel);

            canvas1 = new Canvas();
            Grid.SetRow(canvas1, 1);
            Grid.SetColumn(canvas1, 1);
            grid.Children.Add(canvas1);


            canvas1.Children.Add(innerGrid);
            innerGrid.ColumnDefinitions.Add(new ColumnDefinition());
            innerGrid.ColumnDefinitions.Add(new ColumnDefinition());

            expanderWindow = new ExpanderWindow(canvasHeight);
            innerGrid.Children.Add(expanderWindow);
            expanderWindow.VerticalAlignment = VerticalAlignment.Top;

            blockViewer = new BlockViewer(canvasHeight, null, this);
            Grid.SetColumn(blockViewer, 1);
            innerGrid.Children.Add(blockViewer);
            blockViewer.VerticalAlignment = VerticalAlignment.Top;
        }

        protected void OnMouseDown(object e, MouseButtonEventArgs args) 
        {
            Open(ref isOpened);
        }
        protected void Open(ref bool opened) 
        {
            canvasHeight = MainWindow.mainWindow.mainCanvas.ActualHeight;
            storyboardFirstPanel.Stop();
            if (opened)
            {
                if(blockViewer.isShown) 
                { 
                    blockViewer.Hide();
                }
                blockViewer.isShown = false;
                doubleAnimationFirstPanel.From = expanderWindow.ActualHeight;
                doubleAnimationFirstPanel.To = 0;
                doubleAnimationFirstPanel.Duration = new Duration(TimeSpan.FromSeconds(((animationAntiSpeed / canvasHeight) * expanderWindow.ActualHeight)));
                storyboardFirstPanel.Begin(expanderWindow);
                storyboardFirstPanel.Begin(blockViewer);
            }
            else 
            {
                doubleAnimationFirstPanel.From = expanderWindow.ActualHeight;
                doubleAnimationFirstPanel.To = canvasHeight;
                expanderWindow.Height = canvasHeight;
                doubleAnimationFirstPanel.Duration = new Duration(TimeSpan.FromSeconds((animationAntiSpeed / canvasHeight) * MathF.Abs((float)(canvasHeight - expanderWindow.ActualHeight))));
                //canvas.Children.Add(expanderWindow);
                storyboardFirstPanel.Begin(expanderWindow);
                storyboardFirstPanel.Begin(blockViewer);
            }
            isOpened = !isOpened;
        }
        private void OnWindowResize(object sender, SizeChangedEventArgs args) 
        {
            UpdateHeight();
        }

        public void UpdateHeight() 
        {
            if (isOpened)
            {
                isOpened = !isOpened;
                blockViewer.scrollViewer.Height = MainWindow.mainWindow.mainCanvas.ActualHeight;
                Open(ref isOpened);
            }
        }
    }
}
