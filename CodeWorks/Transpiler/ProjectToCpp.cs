using System;
using System.Collections.Generic;
using System.Text;
using FESScript2.Graphics.UserControls;
using System.IO;
using System.Linq;
using FESScript2.Graphics.UserControls.SubUserControls;

namespace FESScript2.CodeWorks.Transpiler
{
    public static class ProjectToCpp
    {
        private static List<Block> initializedBlocks = new List<Block>();
        public static void GenerateCppFile(Block start) 
        {
            if (start != null) 
            { 
                string commands = "";
                string projectFileName = $@"\{Directories.programName}.cpp";
                string path = Directories.Projects + @"\" + Directories.SaveName;
                if (start.dots[0].ConnectedTo != null) 
                { 
                    string contents = InitializeBlock(start.dots[0].ConnectedTo.BlockParent, ref commands);
                    
                    string fileString = 
$@"#include ""{GenerateFullCpp.fileName}.h""
int main() 
{{
{contents}
}}";
                    Directory.CreateDirectory(path);
                    File.WriteAllText(path + projectFileName, fileString);
                } 
                else 
                {
                    string fileString =@$"
int main() 
{{
}}";
                    File.WriteAllText(path + projectFileName, fileString);
                }
            }
            initializedBlocks = new List<Block>();
        }
        private static string InitializeBlock(Block block, ref string returnValue, bool shouldRunLoop = false, bool notContinue = false, bool reinitialize = false) 
        {
            if (!initializedBlocks.Contains(block) || reinitialize)
            {
                initializedBlocks.Add(block);
                List<Block> blocksToRun = new List<Block>();
                foreach (Dots dot in block.InputNonActionDots)
                {
                    if (dot.ConnectedTo != null)
                    {
                        blocksToRun.Add(dot.ConnectedTo.BlockParent);
                    }
                }
                blocksToRun = blocksToRun.Distinct().ToList();
                foreach (Block blockX in blocksToRun)
                {
                    if (!blockX.blockType.IsBodyless)
                    {
                        InitializeBlock(blockX, ref returnValue, reinitialize: reinitialize);
                    }
                }
                string constructorParams = "";
                for (int i = 0; i < block.dots.Count; i++)
                {
                    if (block.dots[i].IO == IO.Input && (block.dots[i].DotType != Graphics.UserControls.SubUserControls.Type.Action && block.dots[i].DotType != Graphics.UserControls.SubUserControls.Type.SubAction))
                    {
                        if (block.dots[i].ConnectedTo != null && !block.dots[i].ConnectedTo.BlockParent.blockType.IsBodyless)
                        {
                            constructorParams += $"{block.dots[i].ConnectedTo.BlockParent.Name}.{block.dots[i].ConnectedTo.Name}, ";
                        }
                        else if(block.dots[i].ConnectedTo == null)
                        {
                            constructorParams += $"{block.dots[i].DotType.DefaultValues()}, ";
                        }
                        else if (block.dots[i].ConnectedTo.BlockParent.blockType.IsBodyless)
                        {
                            string x = block.dots[i].ConnectedTo.BlockParent.GetValueOfRelatives(block.dots[i].ConnectedTo.BlockParent.blockType.FakeString);
                            constructorParams += x + ", ";
                        }
                    }
                }
                if (constructorParams != "")
                {
                    constructorParams = $"({constructorParams.Substring(0, constructorParams.Length - 2)})";
                }
                string interactvieContentsSetter = "";
                foreach (IContents content in block.contentsInteractive)
                {
                    if (!content.IsCompiler)
                    {
                        if (content.QuotationMarks)
                        {
                            interactvieContentsSetter += $@"{block.Name}.{content.Name} = ""{content.Text}"";
";
                        }
                        else
                        {
                            interactvieContentsSetter += $@"{block.Name}.{content.Name} = {content.Text};
";
                        }
                    }
                }
                if (block.blockType.CreateFunction && !block.blockType.IsBodyless)
                {
                string declaration = "";
                if(reinitialize && constructorParams == "") 
                {

                }
                else if (reinitialize)
                {
                    declaration = $@"  {block.Name} = {block.blockType.Name}{constructorParams};";
                }
                else 
                {
                    declaration = $@"  {block.blockType.Name} {block.Name}{constructorParams};";
                }
                if (block.ActionoutputDots.Count == 0 || notContinue)
                {
                    returnValue += $@"  {declaration}
    {interactvieContentsSetter}
    {block.Name}.Run();
";
                }
                else if (block.ActionoutputDots.Count == 1 || notContinue)
                {
                    returnValue += $@"  {declaration}
    {interactvieContentsSetter}
    {block.Name}.Run();
";
                    if (block.ActionoutputDots[0].ConnectedTo != null)
                    {
                        block = block.ActionoutputDots[0].ConnectedTo.BlockParent;
                        InitializeBlock(block, ref returnValue, reinitialize: reinitialize);
                    }

                }
                else if (block.ActionoutputDots.Count > 1 || notContinue)
                {
                    string cases = "";
                    string afterCases = "";
                    bool multipleSub = false;
                    string conditionString = "";
                    Dots conditonDot = block.InputNonActionDots.Find(x => x.isConditional == true);
                    if (conditonDot != null && conditonDot.ConnectedTo != null)
                    {
                        InitializeBlock(conditonDot.ConnectedTo.BlockParent, ref conditionString, notContinue: true, reinitialize:true);
                    }
                    for (int i = 0; i < block.ActionoutputDots.Count; i++)
                    {
                        string nextblocks = "";
                        if (block.ActionoutputDots[i].ConnectedTo != null)
                        {
                            Block subBlock = block.ActionoutputDots[i].ConnectedTo.BlockParent;
                            InitializeBlock(subBlock, ref nextblocks, reinitialize: reinitialize);
                            if (block.ActionoutputDots[i].DotType == Graphics.UserControls.SubUserControls.Type.Action)
                            {
                                cases += $@"case {block.ActionoutputDots[i].ID}:
    {{
        {nextblocks}
        break;
    }}
";
                            }
                            else if (block.ActionoutputDots[i].DotType == Graphics.UserControls.SubUserControls.Type.SubAction && !multipleSub)
                            {
                                multipleSub = true;
                                afterCases += $@"{conditionString}
int {block.Name}condition = {block.Name}.Run();
while({block.Name}condition == {block.ActionoutputDots[i].ID}) 
    {{
        {nextblocks}

        {conditionString}

        {declaration}

        {block.Name}condition = {block.Name}.Run();
    }}
";
                            }
                            else if (block.ActionoutputDots[i].DotType == Graphics.UserControls.SubUserControls.Type.SubAction && multipleSub)
                            {
                                afterCases += $@"while({block.Name}condition == {block.ActionoutputDots[i].ID}) 
{{
    {nextblocks}

    {conditionString}

    {declaration}
    {block.Name}condition = {block.Name}.Run();
}}
";
                                }
                        }
                    }
                    if (afterCases == "")
                    {
                        afterCases = $"int {block.Name}condition = {block.Name}.Run();";
                    }
                    returnValue += $@"  {declaration} 
    {interactvieContentsSetter}
    {afterCases}
    switch ({block.Name}condition) 
    {{
        {cases}
    }}
";
                    }
                }
                else if (!block.blockType.IsBodyless)
                {
                    returnValue += $@"  {block.GetValueOfRelatives(block.blockType.FakeString)}
"; 
                    if (block.ActionoutputDots.Count != 0) 
                    { 
                        if (block.ActionoutputDots[0].ConnectedTo != null)
                        {
                            block = block.ActionoutputDots[0].ConnectedTo.BlockParent;
                            InitializeBlock(block, ref returnValue, reinitialize);
                        }
                    }
                }
            }
            return returnValue;
        }
    }
}

