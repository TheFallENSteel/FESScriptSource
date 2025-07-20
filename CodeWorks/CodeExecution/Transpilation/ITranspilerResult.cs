using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FESScript.CodeWorks.CodeExecution.Transpilation
{
    public interface ITranspilerResult
    {
        public string OutputPath { get; }
        public bool Result { get; }
    }
}
