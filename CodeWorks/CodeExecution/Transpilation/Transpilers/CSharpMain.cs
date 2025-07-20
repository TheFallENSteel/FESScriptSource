using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
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
            using (StreamWriter writer = new StreamWriter("output.cs"))
            {
                try
                {
                    await TraverseBlock(startBlock, writer);
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

        private async Task TraverseBlock(BlockPlacement blockPlacement, StreamWriter writer) 
        { 

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
     CodeBlock - is copied from block, addresses are replaced to represent real values in code
     */
}
