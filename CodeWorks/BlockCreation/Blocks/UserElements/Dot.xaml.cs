using FESScript.CodeWorks.BlockCreation.Blocks.Placements;
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

namespace FESScript.CodeWorks.BlockCreation.Blocks.UserElements
{
    /// <summary>
    /// Interaction logic for Dot.xaml
    /// </summary>
    public partial class Dot : UserControl
    {
        public DotPlacement DotPlacement { get; set; }

        private MouseButtonEventHandler mouseDownHandler;
        public MouseButtonEventHandler MouseDownHandler 
        {
            get => mouseDownHandler;
            set
            {
                if (MouseDownHandler != null) this.MouseDown -= MouseDownHandler;
                this.mouseDownHandler = value;
                this.MouseDown += MouseDownHandler;
            }
        }

        private MouseButtonEventHandler mouseUpHandler;
        public MouseButtonEventHandler MouseUpHandler 
        { 
            get => this.mouseUpHandler;
            set 
            { 
                if (MouseUpHandler != null) this.MouseUp -= MouseUpHandler;
                this.mouseUpHandler = value;
                this.MouseUp += MouseUpHandler;
            }
        }

        public void Remove() 
        {

        }

        public Dot()
        {
            InitializeComponent();
        }
        public Dot(DotPlacement dotPlacement)
        {
            DotPlacement = dotPlacement;
            DotPlacement.Dot = this;
            this.DataContext = this.DotPlacement.DotTemplate;
            InitializeComponent();
        }
    }
}
