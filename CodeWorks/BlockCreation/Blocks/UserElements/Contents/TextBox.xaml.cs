using System.Windows.Controls;
using System.Windows.Input;
using FESScript.CodeWorks.BlockCreation.Blocks.Templates;
using FESScript.CodeWorks.BlockCreation.Blocks.Placements;
using FESScript.CodeWorks.BlockCreation.Blocks.UserElements.Contents.Args;

namespace FESScript.CodeWorks.BlockCreation.Blocks.UserElements.Contents
{
    /// <summary>
    /// Interaction logic for TextBox.xaml
    /// </summary>
    public partial class TextBox : UserControl
    {
        public ContentPlacement ContentPlacement { get; set; }

        public TextBox(ContentPlacement contentPlacement)
        {
            if (contentPlacement.Value is not StringArgs)
            {
                contentPlacement.Value = new StringArgs("");
            }
            this.ContentPlacement = contentPlacement;
            this.DataContext = ContentPlacement;
            InitializeComponent();
        }
        public override string ToString() => nameof(TextBox);
    }
}
