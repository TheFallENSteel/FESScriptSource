using FESScript.CodeWorks.BlockCreation.Blocks.Templates;
using FESScript.CodeWorks.BlockCreation.Blocks.UserElements.Contents.Args;
using FESScript.CodeWorks.BlockCreation.Interfaces;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;

namespace FESScript.CodeWorks.BlockCreation.Blocks.Placements
{
    public class ContentPlacement : IFindable, IInfo, INotifyPropertyChanged, IRemovable
    {
        public ContentTemplate ContentData { get; set; }

        public int ID { get; set; }
        public IFindable Parent { get; set; }
        public string Name { get => ContentData.Name; }
        public string Description { get => ContentData.Description; }

        public UserControl Content { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private IArgs value;

        public IArgs Value 
        { 
            get 
            {
                return value;
            }
            set 
            { 
                this.value = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value)));
            } 
        }

        public IArgs DirectValue
        {
            get => ContentData.InitialValue;
            set 
            { 
                ContentData.InitialValue = value; 
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value))); 
            }
        }
        public string FullName() => $"{(Parent as BlockPlacement).FullName()}_C{nameof(ContentData.Type).Take(3)}{ID.ToString()})";
        private ContentPlacement() { }
        public ContentPlacement(ContentTemplate contentData, IFindable parent)
        {
            this.ContentData = contentData;
            this.Value = contentData.InitialValue;
            this.Parent = parent;
            ContentData.PropertyChanged += ChangeType;
            ChangeType(this, new PropertyChangedEventArgs(nameof(ContentData.Type)));
        }

        public static ContentPlacement ModifyType(ContentTemplate contentData, IFindable parent)
        {
            ContentPlacement returnValue = new ContentPlacement();
            returnValue.ContentData = contentData;
            returnValue.Parent = parent;
            returnValue.ContentData.PropertyChanged += returnValue.ChangeType;
            returnValue.ChangeType(returnValue, new PropertyChangedEventArgs(nameof(ContentData.Type)));
            return returnValue;
        }

        public void ChangeType(object sender, PropertyChangedEventArgs args) 
        {
            if (args.PropertyName == nameof(ContentData.Type)) 
            { 
                this.Content = (UserControl)Activator.CreateInstance(ContentData.Type, [this]);
            }
            PropertyChanged?.Invoke(sender, args);
            (Parent as BlockPlacement)?.Block?.UpdatePanels();
        }

        public void Remove()
        {
            ContentData.PropertyChanged -= ChangeType;
        }

        public IFindable FindChild(int ID) => null;

    }
}
