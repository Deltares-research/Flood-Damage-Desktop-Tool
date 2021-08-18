using System;
using System.Diagnostics;
using System.IO;
using FDT.Backend.IExeHandler;

namespace FDT.Backend.ExeHandler
{
    public class FiatPythonWrapper : IExeWrapper
    {
        private readonly string _exeFileName = "fiat_objects.exe";

        public string ExeDirectory { get; set; }
        public string ExeFilePath => Path.Combine(ExeDirectory, _exeFileName);

        public FiatPythonWrapper()
        {
            // By default the exe will be located at the same level as the exe where we are working.
            ExeDirectory = Directory.GetCurrentDirectory();
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