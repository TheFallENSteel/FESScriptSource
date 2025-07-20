using System;
using System.Diagnostics;
using System.IO;

namespace FESScript.CodeWorks.CodeExecution.Compilation.Compilers
{
    internal class ExecutionData : IExecutionData
    {
        private Process _process;
        public StreamReader Output { get => _process.StandardOutput; }
        public StreamReader Error { get => _process.StandardError; }
        public StreamWriter Input { get => _process.StandardInput; }
        public bool IsRunning { get => _process != null && !_process.HasExited; }
        public int? ExitCode { get => _process?.ExitCode; }
        public ExecutionData(Process process)
        {
            _process = process ?? throw new ArgumentNullException(nameof(process), "Process cannot be null.");
            if (!_process.StartInfo.RedirectStandardOutput || !_process.StartInfo.RedirectStandardError || !_process.StartInfo.RedirectStandardInput)
            {
                throw new InvalidOperationException("Process must have standard input, output and error redirection enabled.");
            }
        }
        public void Stop()
        {
            if (IsRunning)
            {
                _process?.Kill();
                Debug.WriteLine("Process killed.");
            }
            else Debug.WriteLine("Process is not running.");
        }
    }
}