namespace FESScript.CodeWorks.CodeExecution.Compilation.Compilers
{
    public class CompilationResult : ICompilationResult
    {
        public string Path { get; set; }
        public bool Result { get; set; }
        public string Output { get; set; }
        public string Error { get; set; }
        public ProgrammingLanguage Language { get; set; }
        public string CompilerName { get; set; }
        public string CompilerVersion { get; set; }
    }
}
