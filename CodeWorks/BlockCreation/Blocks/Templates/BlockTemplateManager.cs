using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FESScript.CodeWorks.BlockCreation.Blocks.Templates
{
    public class BlockTemplateManager
    {
        private List<BlockTemplate> blockTemplates = new List<BlockTemplate>();
        public BlockTemplateFactory TemplateFactory { get; private set; } = new BlockTemplateFactory();

        public BlockTemplate CreateTemplate(ObservableCollection<DotTemplate> dots, ObservableCollection<ContentTemplate> contents, Type type, string name = "", string inLineBodyCode = null, string description = "")
        {
            BlockTemplate template = TemplateFactory.CreateTemplate(dots, contents, type, name, inLineBodyCode, description);
            blockTemplates.Add(template);
            return template;
        }
        public BlockTemplate CreateTemplate(Type type, string name = "", string inLineBodyCode = null, string description = "")
        {
            BlockTemplate template = TemplateFactory.CreateTemplate(new ObservableCollection<DotTemplate>(), new ObservableCollection<ContentTemplate>(), type, name, inLineBodyCode, description);
            blockTemplates.Add(template);
            return template;
        }
        public BlockTemplate CreateTemplate()
        { 
            BlockTemplate template = TemplateFactory.CreateEmptyTemplate();
            blockTemplates.Add(template);
            return template;
        }
        public void RemoveTemplate(BlockTemplate template)
        {
            if (blockTemplates.Contains(template))
            {
                blockTemplates.Remove(template);
            }
        }
        public BlockTemplate GetTemplateById(int id)
        {
            return blockTemplates.FirstOrDefault(t => t.ID == id);
        }
    }
}
