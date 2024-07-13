using System.Windows.Controls;
using FESScript.CodeWorks.BlockCreation.Blocks.Templates;
using FESScript.CodeWorks.BlockCreation.Blocks.Placements;
using FESScript.CodeWorks.BlockCreation.Blocks.UserElements.Contents.Args;

namespace FESScript.CodeWorks.BlockCreation.Blocks.UserElements.Contents
{
    /// <summary>
    /// Interaction logic for CheckBox.xaml
    /// </summary>
    public partial class CheckBox : UserControl
    {
        public ContentPlacement ContentPlacement { get; set; }

        public CheckBox(ContentPlacement contentPlacement)
        {
            if (contentPlacement.Value is not BoolArgs)
            {
                contentPlacement.Value = new BoolArgs(false);
            }
            this.ContentPlacement = contentPlacement;
            this.DataContext = ContentPlacement;
            InitializeComponent();
        }
        public override string ToString() => nameof(CheckBox);
    }
}

