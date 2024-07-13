using FESScript.CodeWorks.BlockCreation.Blocks.Templates;
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
    public partial class DotDisplay : DataDisplay
    {
        public DotTemplate DotTemplate { get; set; }

        public DotDisplay(DotTemplate template)
        {
            InitializeComponent();
            this.DotTemplate = template;
            this.DataContext = template;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public override object GetData()
        {
            return this.DotTemplate;
        }

        public override void SetData(object data)
        {
            Debug.Assert(data is DotTemplate);
            DotTemplate = data as DotTemplate;
        }

        private void IOChanged(object sender, SelectionChangedEventArgs e)
        {
            (DotTemplate.Parent as BlockTemplate)?.Update();
        }
    }
}

/*
    [XmlAttribute] public int ID { get; set; } = iD;

    [XmlAttribute] public string Name { get; set; } = name;
    public string Description { get; set; } = description;

    [XmlAttribute] public Type Type { get; set; } = type;
    [XmlAttribute] public IO IO { get; set; } = iO;

    public string DotCopyCode { get; set; } = dotCopyCode;

    public DotTemplate() : this(Type.Error, IO.Error, -1) { }
    public IFindable FindChild(int ID) => null;
 */
