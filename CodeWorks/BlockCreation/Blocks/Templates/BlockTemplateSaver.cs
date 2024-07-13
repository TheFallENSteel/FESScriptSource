using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace FESScript.CodeWorks.BlockCreation.Blocks.Templates
{
    public class BlockTemplateSaver
    {
        public string CategoriesDirectory { get; set; }

        public BlockTemplateSaver(string categoriesDirectory)
        {
            CategoriesDirectory = categoriesDirectory;
        }

        public void SaveTemplate(BlockTemplate blockTemplate) 
        {
            FileStream writer = File.Open(Path.Combine(CategoriesDirectory, $"{blockTemplate.ID}.{BlockTemplateLoader.FileExtension}"), FileMode.Create);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(BlockTemplate));
            xmlSerializer.Serialize(writer, blockTemplate);
            writer.Close();
        }
    }
}
