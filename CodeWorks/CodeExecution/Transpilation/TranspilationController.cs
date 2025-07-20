using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FESScript.CodeWorks.BlockCreation.Blocks.Placements;

namespace FESScript.CodeWorks.CodeExecution.Transpilation
{
    public class TranspilationController
    {
        public ITranspilerResult? LastResult { get; private set; } = null;
        public ITranspiler Transpiler { get; private set; }
        public bool IsSet => Transpiler != null;
        public async Task<bool?> Transpile(BlockPlacement startBlockPlacement)
        {
            var result = await Transpiler.Transpile(startBlockPlacement);
            LastResult = result;
            return LastResult?.Result;
        }
        public void SetTranspiler(ITranspiler transpiler)
        {
            Transpiler = transpiler;
            LastResult = null;
        }
        static TranspilationController()
        {
            _ = Transpilers.CSharpMain.Instance;
        }
    }
}
