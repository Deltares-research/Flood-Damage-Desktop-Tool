using System;
using System.Collections.Generic;
using System.Linq;
using FDT.Backend.DomainLayer.IDataModel;
using FDT.Backend.PersistenceLayer;
using FDT.Backend.PersistenceLayer.FileObjectModel;
using FDT.Backend.PersistenceLayer.IFileObjectModel;
using FDT.Backend.Properties;
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
        private readonly XlsxDataWriter _dataWriter;
        public virtual IFloodDamageDomain DataDomain { get; set; }
        public virtual IWriter DataWriter => _dataWriter;
        public virtual IExeWrapper ExeWrapper => _exeWrapper;

        public bool WriteShpOutput
        {
            set => _dataWriter.SaveOutput = value;
        }

        public DamageAssessmentHandler()
        {
            _dataWriter = new XlsxDataWriter();
            _exeWrapper = new FiatPythonWrapper();
            WriteShpOutput = false;
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
                    string errorHeader = string.Format(Resources.DamageAssessmentHandler_Run_Error_while_running_basin___0___scenario___1_, outputData.BasinName, outputData.ScenarioName);
                    string reportErrors = String.Join("\n", exeReport.IssueList);
                    errorRuns.Add(string.Format(Resources.DamageAssessmentHandler_Run_ErrorLine, errorHeader, reportErrors));
                }
            }

            if (!errorRuns.Any()) return;

            string errorRun = string.Join("\n\n", errorRuns);
            throw new Exception(errorRun);
        }
    }
}