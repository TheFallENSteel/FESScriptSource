using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace FESScript2
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        public delegate void CategoryEventHandler(CodeWorks.BlockCreation.Category category);
        public static event CategoryEventHandler CategoryChanged;

        private void BlockViewer_OnMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            CategoryChanged?.Invoke((CodeWorks.BlockCreation.Category)((System.Windows.Controls.Label)sender).DataContext);
        }
    }
}
