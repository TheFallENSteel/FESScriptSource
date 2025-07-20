using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Controls.Primitives;
using System.Windows.Shapes;
using FESScript.CodeWorks.CodeExecution.Compilation;
using FESScript.CodeWorks.CodeExecution.Transpilation;

namespace FESScript
{
    public class Settings
    {
        private ICompiler _compiler;
        private ITranspiler _transpiler;

        public ICompiler Compiler { get => _compiler ?? ICompiler.DefaultCompiler; set => _compiler = value; }
        public ITranspiler Transpiler { get => _transpiler ?? ITranspiler.DefaultTranspiler; set => _transpiler = value; }

    }
}
