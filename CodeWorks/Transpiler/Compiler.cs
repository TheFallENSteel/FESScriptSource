using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace FESScript2.CodeWorks.Transpiler
{
    public static class Compiler
    {

        /// <summary>
        /// Compiles C++ file using g++ compiler.
        /// </summary>
        /// <param name="fileName">Name of the file to compile.</param>
        /// <param name="filePath">File directory.</param>
        /// <returns>Returns false if operation fails.</returns>

        /*public static bool TranspileLibrary(string fileName, string filePath) 
        {
            Process ConsoleCpp = new Process();
            ConsoleCpp.StartInfo = new ProcessStartInfo("CMD.EXE", null);
            ConsoleCpp.StartInfo.RedirectStandardInput = true;
            ConsoleCpp.StartInfo.WorkingDirectory = filePath;
            ConsoleCpp.StartInfo.CreateNoWindow = false;
            ConsoleCpp.Start();
            File.Delete(Path.Combine(filePath, fileName + ".o"));
            StreamWriter writer = ConsoleCpp.StandardInput;
            writer.WriteLine($"g++ -o {fileName}.o -c {fileName}.cpp");
            writer.WriteLine($"ar rvs {fileName}.a {fileName}.o");
            ConsoleCpp.Close();
            writer.Close();
            return true;
        }*/
        const string filePath = @"AppFiles\Projects";
        const string libraryPath = @"AppFiles\Blocks\Precompiled";
        public static bool CompileProject(string fileName, string directoryPath)
        {
            Process ConsoleCpp = new Process();
            ConsoleCpp.StartInfo = new ProcessStartInfo("CMD.EXE", null);
            ConsoleCpp.StartInfo.RedirectStandardInput = true;
            ConsoleCpp.StartInfo.WorkingDirectory = directoryPath + @"\";// + filePath + @"\" + Directories.SaveName;
            ConsoleCpp.StartInfo.CreateNoWindow = true;
#if DEBUG
            ConsoleCpp.StartInfo.CreateNoWindow = false;
#endif
            ConsoleCpp.Start();
            StreamWriter writer = ConsoleCpp.StandardInput;
            writer.WriteLine(@$"del ""{filePath + @"\" + Directories.SaveName + @"\" + GenerateFullCpp.fileName}.exe""");
            writer.WriteLine(@$"g++ {filePath + @"\" + Directories.SaveName + @"\" + Directories.programName}.cpp ""{libraryPath}\{GenerateFullCpp.fileName}.cpp"" -I ""{libraryPath}"" -o {filePath + @"\" + Directories.SaveName + @"\" + GenerateFullCpp.fileName}.exe");
            ConsoleCpp.Close();
            writer.Close();
            return true;
            //ěšžčřčž
        }
    }
}
