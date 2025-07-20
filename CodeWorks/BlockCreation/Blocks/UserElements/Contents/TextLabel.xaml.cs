using System.Windows.Controls;
using FESScript.CodeWorks.BlockCreation.Blocks.Templates;
using FESScript.CodeWorks.BlockCreation.Blocks.Placements;
using FESScript.CodeWorks.BlockCreation.Blocks.UserElements.Contents.Args;

namespace FESScript.CodeWorks.BlockCreation.Blocks.UserElements.Contents
{
    /// <summary>
    /// Interaction logic for TextLabel.xaml
    /// </summary>
    public partial class TextLabel : UserControl
    {
        public ContentPlacement ContentPlacement { get; set; }

        public TextLabel(ContentPlacement contentPlacement)
        {
            if (contentPlacement.Value is not StringArgs)
            {
                contentPlacement.Value = new StringArgs("", true);
            }
            contentPlacement.Value.IsReadOnly = true;
            this.ContentPlacement = contentPlacement;
            this.DataContext = ContentPlacement;
            InitializeComponent();
        }
        public override string ToString() => nameof(TextLabel);
    }
}
