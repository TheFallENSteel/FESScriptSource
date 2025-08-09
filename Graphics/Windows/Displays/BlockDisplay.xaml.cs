using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using FESScript.CodeWorks.BlockCreation.Blocks.Templates;
using FESScript.Graphics.Windows.Constructors;

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
            var template = new DotTemplate(BlockTemplate);
            this.BlockTemplate.Dots.Add(template);
            this.BlockTemplate.Update(template, new PropertyChangedEventArgs(template.ID.ToString()));
        }
        private void AddContentClick(object sender, RoutedEventArgs e)
        {
            var template = new ContentTemplate(BlockTemplate);
            this.BlockTemplate.Contents.Add(template);
            this.BlockTemplate.Update(template, new PropertyChangedEventArgs(template.ID.ToString()));
        }

        private void DotDelete(object sender, RoutedEventArgs e)
        {
            var template = (sender as Button).DataContext as DotTemplate;
            this.BlockTemplate.Dots.Remove(template);
            this.BlockTemplate.Update(template, new PropertyChangedEventArgs(template.ID.ToString()));
        }
        private void ContentDelete(object sender, RoutedEventArgs e)
        {
            ContentTemplate template = (sender as Button).DataContext as ContentTemplate;
            this.BlockTemplate.Contents.Remove((sender as Button).DataContext as ContentTemplate);
            this.BlockTemplate.Update(template, new PropertyChangedEventArgs(template.ID.ToString()));
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
        private List<FileSystemWatcher> _watcher = new List<FileSystemWatcher>(8);
        private void EditCode(object sender, RoutedEventArgs e)
        {
            try 
            { 
                string code = BlockTemplate.InBlockCode;
                string fileName = $"{BlockTemplate.Name}.cs";
                using (StreamWriter writer = new StreamWriter(fileName, new FileStreamOptions() { Mode = FileMode.OpenOrCreate, Access = FileAccess.Write, Share = FileShare.ReadWrite }))
                {
                    writer.Write(code);
                }
                Process process = Process.Start(new ProcessStartInfo
                {
                    FileName = fileName,
                    UseShellExecute = true,
                    Verb = "open",
                });
                process.EnableRaisingEvents = true;
                FileSystemWatcher watcher = new FileSystemWatcher()
                {
                    Filter = fileName,
                    NotifyFilter = NotifyFilters.LastWrite,
                };
                watcher.Path = Path.GetDirectoryName(Path.GetFullPath(fileName));
                watcher.EnableRaisingEvents = true;
                process.Exited += (s, e) =>
                {
                    watcher.EnableRaisingEvents = false;
                    _watcher.Remove(watcher);
                    watcher.Dispose();
                    Debug.WriteLine($"File {fileName} closed, updating block code.");
                    SaveCode(s, new FileSystemEventArgs(WatcherChangeTypes.Changed, watcher.Path, watcher.Filter), fileName);
                    File.Delete(fileName);
                };
                _watcher.Add(watcher);
                watcher.Changed += (s, e) =>
                {
                    SaveCode(s, e, fileName);
                };
            }
            catch (Exception ex) 
            {
                Debug.WriteLine($"Cannot open code editor: {ex.Message}");
                MessageBox.Show($"Cannot open code editor: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void SaveCode(object s, FileSystemEventArgs e, string fileName) 
        {
            try
            {
                if (e.Name == fileName)
                {
                    using (StreamReader reader = new StreamReader(e.Name, new FileStreamOptions() { Share = FileShare.ReadWrite, Access = FileAccess.Read, Mode = FileMode.Open }))
                    {
                        string code = reader.ReadToEnd();
                        Dispatcher.Invoke(() =>
                        {
                            BlockTemplate.InBlockCode = code;
                        });
                    }
                }
                Debug.WriteLine($"File {e.FullPath} saved, updating block code.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Cannot save file: {e.FullPath}: {ex.Message}");
            }
        }
    }
}
