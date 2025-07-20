using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FESScript.CodeWorks.CodeExecution.Transpilation;

namespace FESScript.CodeWorks.CodeExecution.Compilation.Compilers
{
    internal class CSharpDotnet : ICompiler
    {
        public static CSharpDotnet Instance { get; } = new CSharpDotnet();
        public ProgrammingLanguage Language { get => ProgrammingLanguage.CSharp; }
        public string Name { get => nameof(CSharpDotnet); }
        private string _version = "1.0";
        public string Version { get => _version; }

        public async Task<ICompilationResult> Compile(ITranspilerResult result)
        {
            var CompilerProcess = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "dotnet",
                    Arguments = $"build \"{result.OutputPath}\" -c Release -o \"{result.OutputPath}\"",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,

                }
            };
            Debug.Write($"Compiling C# code with dotnet: {CompilerProcess.StartInfo.FileName}---{CompilerProcess.StartInfo.Arguments}");
            CompilerProcess.Start();
            string output = await CompilerProcess.StandardOutput.ReadToEndAsync();
            string error = await CompilerProcess.StandardError.ReadToEndAsync();
            CompilerProcess.WaitForExit();
            return new CompilationResult
            {
                Result = CompilerProcess.ExitCode == 0,
                Output = output,
                Error = error,
                Language = Language,
                CompilerName = Name,
                CompilerVersion = Version,
                Path = result.OutputPath,
            };
        }

        public IExecutionData Run(ICompilationResult compilationArgs)
        {
            Process process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "dotnet",
                    Arguments = $"run --project \"{compilationArgs.Path}\"",
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };
            Debug.Write($"Running C# code with dotnet: {process.StartInfo.FileName}---{process.StartInfo.Arguments}");
            process.Start();
            return new ExecutionData(process);
        }
        private CSharpDotnet()
        {
            (this as ICompiler).RegisterCompiler(this);
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "dotnet",
                Arguments = "--version",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };
            using (Process process = new Process { StartInfo = startInfo })
            {
                process.Start();
                _version = process.StandardOutput.ReadLine() ?? "Unknown";
                Debug.WriteLine($"C# Dotnet Compiler Version: {_version}");
            }
        }
    }
}
