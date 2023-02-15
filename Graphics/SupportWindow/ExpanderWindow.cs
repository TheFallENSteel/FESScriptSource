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
    public class ExpanderWindow : UserControl
    {
        StackPanel stackPanel;
        public const int width = 150;
        public ExpanderWindow(double height) 
        {
            ItemsControl itemsControl = new ItemsControl();
            itemsControl.ItemsSource = CodeWorks.BlockCreation.Category.categories;
            itemsControl.ItemTemplate = (DataTemplate)Application.Current.Resources["CategoryNameShower"];
            stackPanel = new StackPanel();
            itemsControl.IsTabStop = false;
            stackPanel.Background = CustomBrushes.OrangeMenu1;
            stackPanel.Children.Add(itemsControl);
            stackPanel.Width = width;
            stackPanel.HorizontalAlignment = HorizontalAlignment.Stretch;
            stackPanel.VerticalAlignment = VerticalAlignment.Stretch;
            stackPanel.SetVerticalOffset(5);
            stackPanel.CanVerticallyScroll = true;
            stackPanel.Orientation = Orientation.Vertical;
            Dispatcher.BeginInvoke( new Action(() => 
            {
                Height = height;
            }), System.Windows.Threading.DispatcherPriority.Loaded);
            ScrollViewer scrollViewer = new ScrollViewer();
            scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
            scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
            scrollViewer.Content = stackPanel;
            this.Content = scrollViewer;
        }
    }
}
