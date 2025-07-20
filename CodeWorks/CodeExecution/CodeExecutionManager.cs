using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FESScript.CodeWorks.BlockCreation.Blocks.Placements;
using FESScript.CodeWorks.CodeExecution.Compilation;
using FESScript.CodeWorks.CodeExecution.Transpilation;

namespace FESScript.CodeWorks.CodeExecution
{
    public class CodeExecutionManager
    {
        public ICompiler Compiler
        {
            get => CompilationController.Compiler;
            set => CompilationController.SetCompiler(value);
        }
        public ITranspiler Transpiler
        {
            get => TranspilationController.Transpiler;
            set => TranspilationController.SetTranspiler(value);
        }
        public bool IsReady => 
            CompilationController.IsSet 
            && TranspilationController.IsSet 
            && CompilationController.Compiler.Language == TranspilationController.Transpiler.Language;
        public CompilationController CompilationController { get; private set; } = new CompilationController();
        public TranspilationController TranspilationController { get; private set; } = new TranspilationController();
        public async Task<bool> CompileExecuteCode(BlockPlacement startBlockPlacement)
        {
            var result = await CompileCode(startBlockPlacement);
            if (result)
            {
                Run();
                return true;
            }
            return false;
        }
        private bool CheckReady()
        {
            if (!IsReady)
            {
                Debug.WriteLine("CodeExecutionManager is not ready. Compiler or Transpiler is not set.");
                return false;
            }
            return true;
        }
        public async Task<bool> CompileCode(BlockPlacement startBlockPlacement)
        {
            bool success = CheckReady() && await TranspilationController.Transpile(startBlockPlacement) == true;
            if (success != true)
            {
                Debug.WriteLine("Transpilation failed.");
                return false;
            }
            Debug.WriteLine("Executing Transpiled Code: " + TranspilationController.LastResult?.Result);
            success = await CompilationController.Compile(TranspilationController.LastResult) == true;
            if (success != true)
            {
                Debug.WriteLine("Compilation failed.");
                return false;
            }
            return true;
        }
        public void Run()
        {
            if (CheckReady())
            {
                CompilationController.Run();
            }
        }
    }
}