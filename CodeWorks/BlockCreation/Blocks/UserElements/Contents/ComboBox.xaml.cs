using System.Windows.Controls;
using FESScript.CodeWorks.BlockCreation.Blocks.Templates;
using FESScript.CodeWorks.BlockCreation.Blocks.Placements;
using System.Collections.Generic;
using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using FESScript.CodeWorks.BlockCreation.Blocks.UserElements.Contents.Args;

namespace FESScript.CodeWorks.BlockCreation.Blocks.UserElements.Contents
{
    /// <summary>
    /// Interaction logic for Combobox.xaml
    /// </summary>
    public partial class Combobox : UserControl
    {
        public ContentPlacement ContentPlacement { get; set; }

        public Combobox(ContentPlacement contentPlacement)
        {
            if (contentPlacement.Value is not ComboBoxArgs) 
            {
                contentPlacement.Value = new ComboBoxArgs();
            }
            this.ContentPlacement = contentPlacement;
            this.DataContext = contentPlacement;
            InitializeComponent();
        }
    }
}
