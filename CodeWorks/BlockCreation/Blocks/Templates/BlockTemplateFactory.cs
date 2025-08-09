using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using FESScript.CodeWorks.BlockCreation.Blocks.UserElements;
using FESScript.CodeWorks.BlockCreation.Interfaces;
using Microsoft.VisualBasic;

namespace FESScript.CodeWorks.BlockCreation.Blocks.Templates
{
    public class BlockTemplateFactory
    {
        public void AddDot(BlockTemplate template, DotTemplate dot)
        {
            template.Dots.Add(dot);
            dot.Parent = template;
            template.Update();
        }
        public void AddContent(BlockTemplate template, ContentTemplate content)
        {
            template.Contents.Add(content);
            content.Parent = template;
            template.Update();
        }
        public void RemoveDot(BlockTemplate template, DotTemplate dot)
        {
            template.Dots.Remove(dot);
            dot.Parent = null;
        }
        public void RemoveContent(BlockTemplate template, ContentTemplate content)
        {
            template.Contents.Remove(content);
            content.Parent = null;
        }
        public void ChangeName(BlockTemplate template, string name)
        {
            template.Name = name;
            template.Update();
        }
        public void ChangeDescription(BlockTemplate template, string description)
        {
            template.Description = description;
            template.Update();
        }
        public void ChangeType(BlockTemplate template, Type type)
        {
            template.Type = type;
            template.Update();
        }
        public void ChangeCode(BlockTemplate template, string inLineBodyCode)
        {
            template.InBlockCode = inLineBodyCode;
            template.Update();
        }

        public BlockTemplate CreateTemplate(ObservableCollection<DotTemplate> dots, ObservableCollection<ContentTemplate> contents, Type type, string name = "", string inLineBodyCode = null, string description = "")
        {
            BlockTemplate template = new BlockTemplate();
            template.Dots = dots;
            template.Contents = contents;
            template.InBlockCode = inLineBodyCode;
            template.Name = name;
            template.Type = type;
            template.Description = description;
            for (int i = 0; i < template.Dots.Count; i++)
            {
                template.Dots[0].Parent = template;
            }
            for (int i = 0; i < template.Contents.Count; i++)
            {
                template.Contents[i].Parent = template;
            }
            (template as IFindable).Register();
            template.Update();
            return template;
        }
        public BlockTemplate CreateEmptyTemplate() 
        {
            return CreateTemplate(
                new ObservableCollection<DotTemplate>(), 
                new ObservableCollection<ContentTemplate>(), 
                Type.Error, 
                "New Template", 
                null, 
                "This is a new block template.");
        }
    }
}
