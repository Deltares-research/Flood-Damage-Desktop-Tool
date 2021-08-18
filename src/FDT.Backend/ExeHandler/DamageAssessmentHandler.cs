using System;
using System.Collections.Generic;
using FDT.Backend.IDataModel;
using FDT.Backend.IExeHandler;
using FDT.Backend.OutputLayer;

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
            IEnumerable<string> damageAssessmentFiles = DataWriter.WriteData(DataDomain);
            
            foreach (string damageAssessmentFile in damageAssessmentFiles)
            {
                ExeWrapper.Run(damageAssessmentFile);
            }
        }
    }
}