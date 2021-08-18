using System;
using System.Diagnostics;
using System.IO;
using FDT.Backend.IExeHandler;
using FDT.Backend.OutputLayer.IFileObjectModel;

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

        public void Run(IOutputData outputData)
        {
            if (outputData == null)
            {
                throw new ArgumentNullException(nameof(outputData));
            }
            if (string.IsNullOrEmpty(outputData.FilePath))
                throw new ArgumentNullException(nameof(IOutputData.FilePath));
            if (!File.Exists(outputData.FilePath))
                throw new FileNotFoundException(outputData.FilePath);

            string arguments = $"--config {outputData.FilePath}";
            // Possibly consider using EnableRaisingEvents and subscribe to Process.Exited.
            Process runProcess = Process.Start(ExeFilePath, arguments);
            runProcess?.WaitForExit();
        }
    }
}