using System.IO;

namespace FESScript.CodeWorks.CodeExecution.Compilation
{
    public interface IExecutionData
    {
        public StreamReader Output { get; }
        public StreamReader Error { get; }
        public StreamWriter Input { get; }
        public bool IsRunning { get; }
        public void Stop();
        public int? ExitCode { get; }
    }
}