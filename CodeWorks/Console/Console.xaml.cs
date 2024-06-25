using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Threading.Tasks;

namespace FESScript2.Console
{
    /// <summary>
    /// Special Console window that runs user programs.
    /// </summary>
    public partial class Console : Window
    {
        /// <summary>
        /// Process that starts cmd and redirects input and output to Console window.
        /// </summary>
        private Process ConsoleCpp;
        /// <summary>
        /// Thread running responses between system and app Console.
        /// </summary>
        private Thread ConsoleThread;
        /// <summary>
        /// Input to Console.
        /// </summary>
        public StreamWriter writer;
        /// <summary>
        /// Determines whether the app Console is opened.
        /// </summary>
        bool isOpened = false;
        public bool ShowErrors = true;


        public Console()
        {
            InitializeComponent();
            StartConsole();
        }

        /// <summary>
        /// Writes line to the App Console.
        /// </summary>
        /// <param name="text">Text that should be printed.</param>

        public void Write(string text) 
        {
            //this.consoleLines.textBlock.Text += $"\n{text}";
            writer.WriteLine(text);
        }

        /// <summary>
        /// Starts the system Console.
        /// </summary>

        private void StartConsole()
        {
            isOpened = true;
            ConsoleCpp = new Process();
            Application.Current.Exit += (sender, args) => { writer.WriteLine("taskkill OutputFile.exe"); ConsoleCpp.Kill(); ConsoleCpp.Close(); };
            ConsoleCpp.StartInfo = new ProcessStartInfo("CMD.EXE", null);
            ConsoleCpp.StartInfo.RedirectStandardInput = true;
            ConsoleCpp.StartInfo.RedirectStandardOutput = true;
            ConsoleCpp.StartInfo.RedirectStandardError = true;
            ConsoleCpp.StartInfo.CreateNoWindow = true;
            ConsoleCpp.StartInfo.StandardInputEncoding = Encoding.UTF8;
            ConsoleCpp.StartInfo.StandardOutputEncoding = Encoding.UTF8;
            ConsoleCpp.StartInfo.StandardErrorEncoding = Encoding.UTF8;
#if DEBUG
            ConsoleCpp.StartInfo.CreateNoWindow = false;
#endif

            string dir = "";
            Dispatcher.Invoke(() =>
            {
                dir = Directories.Projects + @"\" + Directories.SaveName;
                ConsoleCpp.StartInfo.WorkingDirectory = dir;
            });
            ConsoleCpp.Start();
            writer = ConsoleCpp.StandardInput;
            //reader = ConsoleCpp.StandardOutput;
            //error = ConsoleCpp.StandardError;
            writer.AutoFlush = true;
            ConsoleThread = new Thread(new ThreadStart(ConsoleResponse));
            ConsoleThread.Start();
            ShowErrors= true;
        }

        /// <summary>
        /// Makes app Console responses.
        /// </summary>

        private void ConsoleResponse() 
        {
            writer.WriteLine("prompt $");
            OpenProgramFile(CodeWorks.Transpiler.GenerateFullCpp.fileName); //ěšžčřčž
            byte triedCount = 0;
            ReadMessage(true);
            ReadError(triedCount);
            while (isOpened && MainWindow.isRunning)
            {
                if (ConsoleCpp.HasExited) 
                {
                    isOpened = false;
                }
                    /*if (error.Peek() != 0)
                    {
                        toRead = reader.ReadLine();
                        if (toRead != "") 
                        { 
                            this.Dispatcher.Invoke(() => { this.consoleLines.textBlock.Text += $"\n{toRead}"; }, System.Windows.Threading.DispatcherPriority.Send);
                        }
                    }
                    if (ShowErrors && error.Peek() != 0) 
                    { 
                        toReadError = error.ReadLine();
                        if (toReadError != "")
                        {
                            if (toReadError == "'OutputFile.exe' is not recognized as an internal or external command," && triedCount < 10)
                            {
                                System.Threading.Thread.Sleep(1000);
                                OpenProgramFile(CodeWorks.Transpiler.GenerateFullCpp.fileName);
                                error.ReadLine();
                                error.ReadLine();
                                triedCount++;
                            }
                            else if (triedCount == 5)
                            {
                                this.Dispatcher.Invoke(() => { this.consoleLines.textBlock.Text += $"\nProgram Not Found"; }, System.Windows.Threading.DispatcherPriority.Send);
                            }
                            else
                            {
                                this.Dispatcher.Invoke(() => { this.consoleLines.textBlock.Text += $"\n{toReadError}"; }, System.Windows.Threading.DispatcherPriority.Send);
                            }
                        }
                    }
                    triedCount++;*/
            }
            CloseConsole();
        }

        private async void ReadMessage(bool initial) 
        {
            if (initial) 
            { 
                for (int i = 0; i < 6; i++) 
                { 
                    ConsoleCpp.StandardOutput.ReadLine();
                }
            }
            string toRead = await ConsoleCpp.StandardOutput.ReadLineAsync();
            if (toRead != "")
            {
                this.Dispatcher.Invoke(() => { this.consoleLines.textBlock.Text += $"\n{toRead}"; }, System.Windows.Threading.DispatcherPriority.Send);
            }
            if (isOpened) 
            {
                ReadMessage(false);
            }
        }

        private async void ReadError(byte triedCount) 
        {
            if (ShowErrors)
            {
                string toReadError = await ConsoleCpp.StandardError.ReadLineAsync();
                this.Dispatcher.Invoke(() => { this.consoleLines.textBlock.Text += $"\n{toReadError}"; }, System.Windows.Threading.DispatcherPriority.Send);
                if (toReadError != "")
                {
                    if (toReadError == "'OutputFile.exe' is not recognized as an internal or external command," && triedCount < 10)
                    {
                        Thread.Sleep(1000);
                        OpenProgramFile(CodeWorks.Transpiler.GenerateFullCpp.fileName);
                        triedCount++;
                    }
                }
            }
            if (isOpened)
            {
                ReadError(triedCount);
            }
        }

        /// <summary>
        /// Closes the system Consoles when app Console is closed.
        /// </summary>

        private void OnClosed(object a, EventArgs e)
        {
            if (writer.BaseStream.CanWrite) 
            { 
                writer.WriteLine("echo Closing console!");
                writer.WriteLine("taskkill OutputFile.exe");
                writer.WriteLine("exit");
            }
            isOpened = false;
        }

        private void OpenProgramFile(string fileName) 
        {
            try 
            { 
                writer.WriteLine($"{fileName}.exe");
            }
            catch 
            { 
            
            }
        }
        private void CloseConsole() 
        {
            writer.WriteLine("taskkill OutputFile.exe");
            writer.Close();
            ConsoleCpp.StandardOutput.Close();
            ConsoleCpp.StandardError.Close();
            ConsoleCpp.Kill();
            ConsoleCpp.Close();
            Thread.CurrentThread.Join();
        }
    }
}
