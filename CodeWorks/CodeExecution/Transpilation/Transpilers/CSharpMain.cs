using System;
using System.Diagnostics;
using System.IO;
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
            using (StreamWriter writer = new StreamWriter("output.cs"))
            {
                try
                {
                    BlockCodePreparer preparer = new BlockCodePreparer(startBlock);
                    await writer.WriteAsync(preparer.GetCode());
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
    }
}
