using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FESScript.CodeWorks.BlockCreation.Blocks.Placements;
using FESScript.CodeWorks.CodeExecution.Compilation;

namespace FESScript.CodeWorks.CodeExecution.Transpilation
{
    public interface ITranspiler
    {
        public static ITranspiler DefaultTranspiler { get => Transpilers.First(); }
        public static List<ITranspiler> Transpilers { get; } = new List<ITranspiler>();
        public ProgrammingLanguage Language { get; }
        public string Name { get; }
        public string Version { get; }
        public Task<ITranspilerResult> Transpile(BlockPlacement blockPlacement);
        public void RegisterTranspiler(ITranspiler transpiler)
        {
            if (!Transpilers.Contains(transpiler))
            {
                Transpilers.Add(transpiler);
            }
        }
        public void UnregisterTranspiler(ITranspiler transpiler)
        {
            Transpilers.Remove(transpiler);
        }
    }
}
