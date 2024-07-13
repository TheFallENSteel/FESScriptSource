using FESScript.CodeWorks.BlockCreation.Blocks.Templates;
using FESScript.CodeWorks.BlockCreation.Interfaces;
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
using Window = FESScript.Graphics.Windows.Constructors.Window;

namespace FESScript.Graphics.Windows.Displays
{
    /// <summary>
    /// Interaction logic for BlockDisplay.xaml
    /// </summary>
    public partial class BlockDisplay : DataDisplay
    {
        public BlockTemplate BlockTemplate { get; set; }

        private Dictionary<DotTemplate, WindowPlacement> DotTemplatesDisplayed = new Dictionary<DotTemplate, WindowPlacement>();
        private Dictionary<ContentTemplate, WindowPlacement> ContentTemplatesDisplayed = new Dictionary<ContentTemplate, WindowPlacement>();

        public BlockDisplay(BlockTemplate template)
        {
            InitializeComponent();
            this.BlockTemplate = template;
            this.DataContext = template;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public override object GetData()
        {
            return this.BlockTemplate;
        }

        public override void SetData(object data)
        {
            Debug.Assert(data is BlockTemplate);
            BlockTemplate = data as BlockTemplate;
        }

        private void AddDotClick(object sender, RoutedEventArgs e)
        {
            this.BlockTemplate.Dots.Add(new DotTemplate());
            this.BlockTemplate.Update();
        }
        private void AddContentClick(object sender, RoutedEventArgs e)
        {
            this.BlockTemplate.Contents.Add(new ContentTemplate() { Parent = BlockTemplate });
            this.BlockTemplate.Update();
        }

        private void DotDelete(object sender, RoutedEventArgs e)
        {
            this.BlockTemplate.Dots.Remove((sender as Button).DataContext as DotTemplate);
            this.BlockTemplate.Update();
        }
        private void ContentDelete(object sender, RoutedEventArgs e)
        {
            this.BlockTemplate.Contents.Remove((sender as Button).DataContext as ContentTemplate);
            this.BlockTemplate.Update();
        }

        private void EditDot(object sender, RoutedEventArgs e)
        {
            var dotTemplate = (sender as Button).DataContext as DotTemplate;
            WindowPlacement dotWindow;
            if (DotTemplatesDisplayed.TryGetValue(dotTemplate, out dotWindow)) 
            { 
                dotWindow.Highlight();
            }
            else 
            { 
                dotWindow = new WindowPlacement(null, App.Window.MainCanvas, new Point(), new DotDisplay(dotTemplate));
                DotTemplatesDisplayed.Add(dotTemplate, dotWindow);
                dotWindow.Window.WindowClosed += (_, _) => DotTemplatesDisplayed.Remove(dotTemplate);
            }
        }

        private void EditContent(object sender, RoutedEventArgs e)
        {
            var contentTemplate = (sender as Button).DataContext as ContentTemplate;
            WindowPlacement contentWindow;
            if (ContentTemplatesDisplayed.TryGetValue(contentTemplate, out contentWindow))
            {
                contentWindow.Highlight();
            }
            else 
            {
                contentWindow = new WindowPlacement(null, App.Window.MainCanvas, new Point(), new ContentDisplay(contentTemplate));
                ContentTemplatesDisplayed.Add(contentTemplate, contentWindow);
                contentWindow.Window.WindowClosed += (_, _) => ContentTemplatesDisplayed.Remove(contentTemplate);
            }
        }
    }
}

/*
    [XmlAttribute] public int ID { get; set; }

    [XmlAttribute] public string Name { get; set; }
    public string Description { get; set; }

    [XmlAttribute] public Type Type { get; set; }

    public string InFunctionBodyCode { get; set; }
    public string InLineBodyCode { get; set; }

    public List<DotTemplate> Dots { get; init; }
    public List<ContentTemplate> Contents { get; init; }
 */
