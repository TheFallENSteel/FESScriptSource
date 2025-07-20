using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using FESScript.CodeWorks.BlockCreation.Blocks.Placements;

namespace FESScript.CodeWorks.CodeExecution.Transpilation.Transpilers
{
    public class CSharpMain : ITranspiler
    {
        public static CSharpMain Instance { get; } = new CSharpMain();
        public ProgrammingLanguage Language { get => ProgrammingLanguage.CSharp; }
        public string Name { get => nameof(CSharpMain); }
        public string Version { get => "1.0.0"; }

        public async Task<ITranspilerResult> Transpile(BlockPlacement startBlock)
        {
            bool success = true;
            await TraverseBlock(startBlock);
            using (StreamWriter writer = new StreamWriter("output.cs"))
            {
                try
                {
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error during transpilation: {ex.Message}");
                    success = false;
                }
            }
            return new CSharpTranspilerResult
            {
                OutputPath = "output.cs",
                Result = success,
            };
        }
        public CSharpMain()
        {
            (this as ITranspiler).RegisterTranspiler(this);
        }

        private async Task<CSharpBlockResult> TraverseBlock(BlockPlacement blockPlacement) 
        {
            BlockCodePreparer preparer = new BlockCodePreparer(blockPlacement);
            using (StreamWriter writer = new StreamWriter("output.cs", true))
            {
                try
                {
                    string code = preparer.GetCode();
                    await writer.WriteLineAsync(code);
                    return new CSharpBlockResult { CodeBlock = code };
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error during block traversal: {ex.Message}");
                    return null;
                }
            }
        }
        
    }
    public class CSharpBlockResult 
    { 
        public string CodeBlock { get; set; }

    }
    /*
     Input dots - no separate entities, their names in code are replaced by source variable names
     Output dots - separate entities, their names are created in block
     Contents - separate entities, their names are created before block
     CodeBlock - is copied from block, addresses are replaced to represent real values in code $$ID$$
     */
    public class BlockCodePreparer 
    {
        public bool WasRun { get; set; } = false;
        public List<DotPlacement> InputDots { get; private set; }
        public List<DotPlacement> OutputDots { get; private set; }
        public List<DotPlacement> OutputActionDots { get; private set; }
        public List<ContentPlacement> Contents { get; private set; }
        public string CodeBlock { get; private set; }
        public BlockCodePreparer(BlockPlacement blockPlacement)
        {
            InputDots = blockPlacement.DotPlacements
                .Where(dot => dot.IO == IO.Input && dot.Type != Type.Action).ToList();
            OutputDots = blockPlacement.DotPlacements
                .Where(dot => dot.IO == IO.Output && dot.Type != Type.Action).ToList();
            OutputActionDots = blockPlacement.DotPlacements
                .Where(dot => dot.IO == IO.Output && dot.Type == Type.Action).ToList();
            Contents = blockPlacement.ContentPlacements.ToList();
            CodeBlock = blockPlacement.BlockTemplate.InBlockCode;
        }
        private Dictionary<Type, string> dataType = new Dictionary<Type, string>()
        {
            { Type.Action, null },
            { Type.SubAction, null },
            { Type.Boolean, "bool" },
            { Type.Console, null },
            { Type.Error, null },
            { Type.Numerical, "double" },
            { Type.Textual, "string" },
        };
        public string GetCode() 
        { 
            StringBuilder builder = new StringBuilder();

            InputDots.ForEach(dot => 
            {
                string dataTypeName = dataType[dot.Type];
                if (dataTypeName != null)
                {
                    string variableName = dot.FullName();
                    string value;
                    if (dot.ConnectedTo != null) value = dot.ConnectedTo.FullName();
                    else value = dot.Type.DefaultValues();
                    builder.AppendLine($"{dataTypeName} {variableName} = {value};");
                }
            });
            Regex regex = new Regex(@"\$\$([a-zA-Z0-9_]+)\$\$");
            regex.Replace(CodeBlock, match =>
            {
                string variableName = match.Groups[0].Value.Trim().ToLower();
                foreach (var content in Contents)
                {
                    if (content.ID.ToString().ToLower() == variableName) return $"{content.FullName()}";
                }
                foreach (var dot in InputDots)
                {
                    if (dot.ID.ToString().ToLower() == variableName) return $"{dot.FullName()}";
                }
                foreach (var dot in OutputDots)
                {
                    if (dot.ID.ToString().ToLower() == variableName) return $"{dot.FullName()}";
                }
                foreach (var nextCodeDot in OutputActionDots)
                {
                    if (nextCodeDot.ID.ToString().ToLower() == variableName && nextCodeDot.ConnectedTo != null) return new BlockCodePreparer(nextCodeDot.ConnectedTo.Parent as BlockPlacement).GetCode();
                }
                return $"$${variableName}$$";
            });

            return builder.ToString();
        }
    }
}
