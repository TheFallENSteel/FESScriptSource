namespace FESScript.CodeWorks.CodeExecution.Transpilation.Transpilers
{
    internal class CSharpTranspilerResult : ITranspilerResult
    {
        public bool Result { get; set; }
        public string OutputPath { get; set; }
    }
}