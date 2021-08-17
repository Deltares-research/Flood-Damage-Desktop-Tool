using System;
using System.Diagnostics;
using System.IO;
using FDT.Backend.IExeHandler;

namespace FDT.Backend.ExeHandler
{
    public class FiatPythonWrapper : IExeWrapper
    {
        private readonly string _exeFileName = "fiat_objects.exe";

        public string ExeFilePath { get; }

        public FiatPythonWrapper(string exeDirectory)
        {
            if (string.IsNullOrEmpty(exeDirectory))
                throw new ArgumentNullException(nameof(exeDirectory));
            if (!Directory.Exists(exeDirectory))
                throw new DirectoryNotFoundException(exeDirectory);


            ExeFilePath = Path.Combine(exeDirectory, _exeFileName);
        }

        public void Run(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentNullException(nameof(filePath));
            if (!File.Exists(filePath))
                throw new FileNotFoundException(filePath);

            string arguments = $"--config {filePath}";
            // Possibly consider using EnableRaisingEvents and subscribe to Process.Exited.
            Process runProcess = Process.Start(ExeFilePath, arguments);
            runProcess?.WaitForExit();
        }
    }
}