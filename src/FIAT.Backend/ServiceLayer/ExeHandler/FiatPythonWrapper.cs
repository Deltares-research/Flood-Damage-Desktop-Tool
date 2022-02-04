using System.Diagnostics;
using System.IO;
using FIAT.Backend.PersistenceLayer.FileObjectModel;
using FIAT.Backend.PersistenceLayer.IFileObjectModel;
using FIAT.Backend.ServiceLayer.IExeHandler;

namespace FIAT.Backend.ServiceLayer.ExeHandler
{
    public class FiatPythonWrapper : IExeWrapper
    {
        private readonly string _exeFileName = "delft_fiat.exe";
        private ValidationReport _exeReport;
        public string ExeDirectory { get; set; }
        public string ExeFilePath => Path.Combine(ExeDirectory, _exeFileName);

        public FiatPythonWrapper()
        {
            // By default the exe will be located at the same level as the exe where we are working.
            ExeDirectory = Directory.GetCurrentDirectory();
            _exeReport = new ValidationReport();
        }

        public void Run(IOutputData outputData)
        {
            outputData.CheckValidParameters();
            string saveShp = outputData.SaveOutput ? "True" : "False";
            string arguments = $"--config {outputData.ConfigurationFilePath} --save_shp {saveShp}";
            // Possibly consider using EnableRaisingEvents and subscribe to Process.Exited.
            // Process runProcess = Process.Start(ExeFilePath, arguments);
            using (Process runProcess = new Process())
            {
                runProcess.StartInfo.FileName = ExeFilePath;
                runProcess.StartInfo.Arguments = arguments;
                // Cannot redirect as it seems to interfere with the exe
                // https://issuetracker.deltares.nl/browse/FPT-139
                // runProcess.StartInfo.RedirectStandardError = true;
                runProcess.Start();
                // _exeReport.AddIssue(runProcess.StandardError.ReadToEnd());
                runProcess?.WaitForExit();
            }
        }

        public ValidationReport GetValidationReport()
        {
            return _exeReport;
        }
    }
}