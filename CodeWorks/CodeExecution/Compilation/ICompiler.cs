using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FESScript.CodeWorks.CodeExecution.Transpilation;

namespace FESScript.CodeWorks.CodeExecution.Compilation
{
    public interface ICompiler
    {
        public static ICompiler DefaultCompiler { get => Compilers.First(); }
        public static List<ICompiler> Compilers { get; } = new List<ICompiler>();
        public ProgrammingLanguage Language { get; }
        public string Name { get; }
        public string Version { get; }
        public Task<ICompilationResult> Compile(ITranspilerResult result);
        public IExecutionData Run(ICompilationResult compilationArgs);
        public void RegisterCompiler(ICompiler compiler)
        {
            if (!Compilers.Contains(compiler))
            {
                Compilers.Add(compiler);
            }
        }
        public void UnregisterCompiler(ICompiler compiler)
        {
            Compilers.Remove(compiler);
        }
    }
}
