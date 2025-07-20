using FESScript.CodeWorks.BlockCreation.Blocks.Templates;
using FESScript.CodeWorks.BlockCreation.Blocks.UserElements.Contents.Args;
using FESScript.Graphics.Windows.Constructors;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
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

namespace FESScript.Graphics.Windows.Displays
{
    /// <summary>
    /// Interaction logic for DotDisplay.xaml
    /// </summary>
    public partial class SettingsDisplay : DataDisplay
    {
        public Settings ContentTemplate { get; set; }

        public SettingsDisplay(Settings template)
        {
            InitializeComponent();
            this.ContentTemplate = template;
            this.DataContext = template;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public override object GetData()
        {
            return this.ContentTemplate;
        }

        public override void SetData(object data)
        {
            Debug.Assert(data is Settings);
            ContentTemplate = data as Settings;
        }
    }
}
