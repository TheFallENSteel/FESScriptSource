using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FESScript.CodeWorks.BlockCreation.Blocks.Placements;
using FESScript.CodeWorks.BlockCreation.Blocks.Templates;
using FESScript.CodeWorks.BlockCreation.Blocks.UserElements.Contents;
using FESScript.CodeWorks.BlockCreation.Blocks.UserElements.Contents.Args;
using FESScript.Graphics.Windows.Displays;
using FESScript;
using System.Windows.Controls;
using System.Windows;
using TextBox = FESScript.CodeWorks.BlockCreation.Blocks.UserElements.Contents.TextBox;
using CheckBox = FESScript.CodeWorks.BlockCreation.Blocks.UserElements.Contents.CheckBox;

namespace FESScript.Prototyping
{
    internal class Prototype
    {
        MainWindow mainWindow;
        Canvas mainCanvas => mainWindow.MainCanvas;
        BlockTemplateManager BlockTemplateManager => mainWindow.BlockTemplateManager;
        public Prototype(MainWindow mainWindow) 
        {
            this.mainWindow = mainWindow;
        }
        public void OnWindowConstruct()
        {
            BlockTemplate blockTemplate = BlockTemplateManager.CreateTemplate();
            BlockTemplateManager.TemplateFactory.AddDot(blockTemplate, new DotTemplate(blockTemplate, Type.Numerical, IO.Output, 4));
            BlockTemplateManager.TemplateFactory.AddDot(blockTemplate, new DotTemplate(blockTemplate, Type.SubAction, IO.Output, 1));
            BlockTemplateManager.TemplateFactory.AddDot(blockTemplate, new DotTemplate(blockTemplate, Type.Action, IO.Input, 2));
            BlockTemplateManager.TemplateFactory.AddDot(blockTemplate, new DotTemplate(blockTemplate, Type.Console, IO.Input, 3));
            BlockTemplateManager.TemplateFactory.AddContent(blockTemplate, new ContentTemplate(blockTemplate, typeof(TextBox), 5, value: new StringArgs("Pche")));
            BlockTemplateManager.TemplateFactory.AddContent(blockTemplate, new ContentTemplate(blockTemplate, typeof(CheckBox), 6, value: new BoolArgs(true)));
            BlockTemplateManager.TemplateFactory.AddContent(blockTemplate, new ContentTemplate(blockTemplate, typeof(TextLabel), 7, value: new StringArgs("Ahojky")));
            BlockTemplateManager.TemplateFactory.AddContent(blockTemplate, new ContentTemplate(blockTemplate, typeof(Combobox), 8, value: new ComboBoxArgs(["Hi", "Hey", "Hate you!", "I Love you! I Hate you! I Love you! I Hate you! I Love you! I Hate you! I Love you! I Hate you! I Love you! I Hate you! I Love you! I Hate you! I Love you! I Hate you!"], -1)));
            blockTemplate.Type = Type.Action;
            BlockPlacement x = new BlockPlacement(
                blockTemplate,
                null,
                mainCanvas);
            var pl = new Graphics.Windows.Constructors.WindowPlacement(null, mainCanvas, new Point(0, 0), new BlockDisplay(blockTemplate));
        }
    }
}
