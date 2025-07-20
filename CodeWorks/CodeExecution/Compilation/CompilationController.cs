using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FESScript.CodeWorks.CodeExecution.Transpilation;

namespace FESScript.CodeWorks.CodeExecution.Compilation
{
    public class CompilationController
    {
        public ICompiler Compiler { get; private set; }
        public ICompilationResult LastCompilationResult { get; private set; }
        public bool IsSet => Compiler != null;
        public async Task<bool?> Compile(ITranspilerResult result)
        {
            var task = await Compiler.Compile(result);
            LastCompilationResult = task;
            return LastCompilationResult?.Result;
        }
        public void Run()
        {
            Compiler.Run(LastCompilationResult);
        }
        public void SetCompiler(ICompiler compiler)
        {
            Compiler = compiler;
        }
        static CompilationController()
        {
            _ = Compilers.CSharpDotnet.Instance;
        }
    }
}
