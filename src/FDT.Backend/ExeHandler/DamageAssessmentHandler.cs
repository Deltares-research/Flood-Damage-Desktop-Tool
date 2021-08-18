using System;
using System.Collections.Generic;
using FDT.Backend.IDataModel;
using FDT.Backend.IExeHandler;
using FDT.Backend.OutputLayer;
using FDT.Backend.OutputLayer.IFileObjectModel;

namespace FDT.Backend.ExeHandler
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
            foreach (IOutputData outputData in outputDataCollection)
            {
                ExeWrapper.Run(outputData);
            }
        }
    }
}