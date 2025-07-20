using System;
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
    public class BlockTemplateLoader
    {
        public string CategoriesDirectory { get; set; }
        public const string FileExtension = "FESBlock";

        public BlockTemplateLoader(string categoriesDirectory)
        {
            CategoriesDirectory = categoriesDirectory;
        }

        public void Load()
        {
            if (CategoriesDirectory == null) return;
            if (!Directory.Exists(CategoriesDirectory))
            {
                Directory.CreateDirectory(CategoriesDirectory);
            }
            foreach (string fileName in Directory.GetFiles(CategoriesDirectory, $"*.{FileExtension}", SearchOption.AllDirectories))
            {
                try
                {
                    LoadTempalte(Directory.GetParent(fileName).FullName, Path.GetFileName(fileName));
                }
                catch (Exception e)
                {
                    Debug.Print($@"While loading file: ""{fileName}"", exception was caught: ""{e}""");
                }
            }
        }

        private BlockTemplate LoadTempalte(string path, string fileName)
        {
            XmlReader reader = XmlReader.Create(Path.Combine(path, fileName));
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(BlockTemplate));
            BlockTemplate template = (BlockTemplate)xmlSerializer.Deserialize(reader);
            reader.Close();
            return template;
        }
    }
}
