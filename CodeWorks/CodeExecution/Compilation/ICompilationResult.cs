using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FESScript.CodeWorks.CodeExecution.Compilation
{
    public interface ICompilationResult
    {
        public string Path { get; }
        public bool Result { get; }
        public string Error { get; }
    }
}
