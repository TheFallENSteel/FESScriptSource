using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Media;
using FESScript2.Graphics.UserControls;
using FESScript2.Graphics;
using System.Windows.Controls;

namespace FESScript2.CodeWorks.BlockCreation
{
    public static class BlockRecreation
    {
        /// <summary>
        /// Specifies in which row is the context grid.
        /// </summary>

        private const int contentGridColumn = 1;

        /// <summary>
        /// Recreates block from template.
        /// </summary>
        /// <param name="blockTemplate">Template with data to recreate block.</param>
        /// <param name="block">Reference to block.</param>

        public static void RecreateBlock(BlockType blockTemplate, out Block block, bool subscribeToEvents = true, bool show = true)
        {
            block = new Block(show, subscribeToEvents);
            block.IsTabStop = false;
            block.grid.ColumnDefinitions.Add(new ColumnDefinition() { Name = "InputDots", Width = System.Windows.GridLength.Auto });
            block.grid.ColumnDefinitions.Add(new ColumnDefinition() { Name = "Contents", Width = System.Windows.GridLength.Auto });
            block.grid.ColumnDefinitions.Add(new ColumnDefinition() { Name = "OutputDots", Width = System.Windows.GridLength.Auto });
            RecreateBackground(blockTemplate.type, ref block);
            RecreateDots(blockTemplate.dots, ref block);
            RecreateContents(blockTemplate.contents, ref block);
            block.BlockTypeId = blockTemplate.id;
            block.blockType = blockTemplate;
            block.Name = blockTemplate.Name + block.Id;
            if (blockTemplate.id == 0)
            {
                Block startPreview = block;
                startPreview.Dispatcher.BeginInvoke(new Action(() =>
                {
                    MainWindow.mainWindow.Start = startPreview;
                }), System.Windows.Threading.DispatcherPriority.Loaded);
            }
        }

        /// <summary>
        /// Creates background for block.
        /// </summary>
        /// <param name="type">Type of block.</param>
        /// <param name="block">Reference to block.</param>

        private static void RecreateBackground(Graphics.UserControls.SubUserControls.Type type, ref Block block)
        {
            Graphics.UserControls.SubUserControls.BlockBackground background = new Graphics.UserControls.SubUserControls.BlockBackground() { Type = type, StrokeThickness = "5", Stroke = CustomBrushes.StrokesBlock1, MinHeight = 25 };
            Grid.SetColumnSpan(background, int.MaxValue);
            Grid.SetRowSpan(background, int.MaxValue);
            block.grid.Children.Add(background);
        }

        /// <summary>
        /// Struct containing data to recreate dots.
        /// </summary>
        /// <param name="dotTypes">Struct containing data to recreate dots.</param>
        /// <param name="block">Reference to block.</param>

        private static void RecreateDots(List<Graphics.UserControls.SubUserControls.DotsType> dotTypes, ref Block block)
        {
            Grid inputGrid = new Grid();
            Grid outputGrid = new Grid();
            inputGrid.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            outputGrid.VerticalAlignment = System.Windows.VerticalAlignment.Center;

            int ioOutCount = 0;
            int ioInCount = 0;
            foreach (Graphics.UserControls.SubUserControls.DotsType dotType in dotTypes)
            {
                Graphics.UserControls.SubUserControls.Dots dot = new Graphics.UserControls.SubUserControls.Dots();
                dot.DotType = dotType.dotType;
                dot.Id = dotType.Id;
                dot.io = dotType.io;
                block.OnMove += dot.OnParentMove;
                dot.BlockParent = block;
                dot.isConditional = dotType.isConditional;
                if (dotType.io == Graphics.UserControls.SubUserControls.IO.Input)
                {
                    inputGrid.RowDefinitions.Add(new RowDefinition());
                    dot.Padding = new System.Windows.Thickness(10, 0, 0, 0);
                    if (dotType.dotType != Graphics.UserControls.SubUserControls.Type.Action && dotType.dotType != Graphics.UserControls.SubUserControls.Type.SubAction)
                    {
                        block.InputNonActionDots.Add(dot);
                    }
                    inputGrid.Children.Add(dot);
                    Grid.SetRow(dot, ioInCount);
                    ioInCount++;
                }
                else if (dotType.io == Graphics.UserControls.SubUserControls.IO.Output)
                {
                    outputGrid.RowDefinitions.Add(new RowDefinition());
                    dot.Padding = new System.Windows.Thickness(0, 0, 10, 0);
                    if (dotType.dotType == Graphics.UserControls.SubUserControls.Type.Action || dotType.dotType == Graphics.UserControls.SubUserControls.Type.SubAction)
                    {
                        block.ActionoutputDots.Add(dot);
                    }
                    outputGrid.Children.Add(dot);
                    Grid.SetRow(dot, ioOutCount);
                    ioOutCount++;
                }
                dot.Name = dotType.Name;
                block.dots.Add(dot);
            }
            block.grid.Children.Add(inputGrid);
            block.grid.Children.Add(outputGrid);

            Grid.SetColumn(inputGrid, 0);
            Grid.SetRow(inputGrid, 0);
            Grid.SetColumn(outputGrid, int.MaxValue);
            Grid.SetRow(outputGrid, 0);
        }

        /// <summary>
        /// Struct containing data to recreate contents.
        /// </summary>
        /// <param name="contentTypes">Struct containing data to recreate contents.</param>
        /// <param name="block">Reference to block.</param>

        private static void RecreateContents(List<Graphics.UserControls.SubUserControls.ContentsType> contentTypes, ref Block block)
        {
            Grid contentGrid = new Grid();
            Grid.SetColumn(contentGrid, contentGridColumn);
            foreach (Graphics.UserControls.SubUserControls.ContentsType contentType in contentTypes)
            {
                contentGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = System.Windows.GridLength.Auto });
                if (contentType.type == typeof(Graphics.UserControls.SubUserControls.TextLabel))
                {
                    Graphics.UserControls.SubUserControls.TextLabel content = new Graphics.UserControls.SubUserControls.TextLabel();
                    SetContent(content, contentType, ref block);
                    contentGrid.Children.Add(content);
                }
                else if (contentType.type == typeof(Graphics.UserControls.SubUserControls.TextBox))
                {
                    Graphics.UserControls.SubUserControls.TextBox content = new Graphics.UserControls.SubUserControls.TextBox();
                    SetContent(content, contentType, ref block);
                    block.contentsInteractive.Add(content);
                    contentGrid.Children.Add(content);
                }
                else if (contentType.type == typeof(Graphics.UserControls.SubUserControls.Checkbox))
                {
                    Graphics.UserControls.SubUserControls.Checkbox content = new Graphics.UserControls.SubUserControls.Checkbox();
                    SetContent(content, contentType, ref block);
                    content.Margin = new System.Windows.Thickness(0, 12.5, 0, 0);
                    block.contentsInteractive.Add(content);
                    contentGrid.Children.Add(content);
                }
                else if (contentType.type == typeof(Graphics.UserControls.SubUserControls.Combobox))
                {
                    Graphics.UserControls.SubUserControls.Combobox content = new Graphics.UserControls.SubUserControls.Combobox();
                    SetContent(content, contentType, ref block);
                    content.ItemsSource = ((Graphics.UserControls.SubUserControls.ContentArgs.ComboBoxArgs)contentType.ContentArgs).elements;
                    block.contentsInteractive.Add(content);
                    contentGrid.Children.Add(content);
                }
            }
            block.grid.Children.Add(contentGrid);
        }
        private static void SetContent(object content, Graphics.UserControls.SubUserControls.ContentsType contentType, ref Block block)
        {
            ((Graphics.UserControls.SubUserControls.IContents)content).Id = contentType.Id;
            ((Graphics.UserControls.SubUserControls.IContents)content).Text = contentType.text;
            ((Graphics.UserControls.SubUserControls.IContents)content).IsCompiler = contentType.isCompiler;
            Grid.SetColumn((Control)content, contentType.collumn);
            ((Control)content).Padding = new System.Windows.Thickness(2.5, 0, 2.5, 0);
        }
    }
}
