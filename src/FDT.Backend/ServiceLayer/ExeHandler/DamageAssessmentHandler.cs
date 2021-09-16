using System;
using System.Collections.Generic;
using System.Linq;
using FDT.Backend.DomainLayer.IDataModel;
using FDT.Backend.PersistenceLayer;
using FDT.Backend.PersistenceLayer.FileObjectModel;
using FDT.Backend.PersistenceLayer.IFileObjectModel;
using FDT.Backend.ServiceLayer.IExeHandler;

namespace FDT.Backend.ServiceLayer.ExeHandler
{
    public class DamageAssessmentHandler: IRunnerHandler
    {
        /// <summary>
        /// Disclaimer, properties made virtual for testing purposes.
        /// Check GivenMultipleAssessmentFilesDoesMultipleExeRuns
        /// </summary>
        private readonly FiatPythonWrapper _exeWrapper;
        public virtual IFloodDamageDomain DataDomain { get; set; }
        public virtual IWriter DataWriter { get; }

        public virtual IExeWrapper ExeWrapper => _exeWrapper;

        public DamageAssessmentHandler()
        {
            DataWriter = new XlsxDataWriter();
            _exeWrapper = new FiatPythonWrapper();
        }

        public void Run()
        {
            if (DataDomain == null)
                throw new ArgumentNullException(nameof(DataDomain));
            
            _exeWrapper.ExeDirectory = DataDomain.Paths.SystemPath;
            IEnumerable<IOutputData> outputDataCollection = DataWriter.WriteData(DataDomain);
            var errorRuns = new List<string>();
            foreach (IOutputData outputData in outputDataCollection)
            {
                ExeWrapper.Run(outputData);
                ValidationReport exeReport = ExeWrapper.GetValidationReport();
                if (exeReport.HasErrors())
                {
                    string errorHeader = $"Error while running basin: {outputData.BasinName}, scenario: {outputData.ScenarioName}";
                    string reportErrors = String.Join("\n", exeReport.IssueList);
                    errorRuns.Add($"{errorHeader}\n Detailed error: {reportErrors}");
                }
            }

            if (!errorRuns.Any()) return;

            string errorRun = string.Join("\n\n", errorRuns);
            throw new Exception(errorRun);
        }
    }
}