using System;
using System.Collections.Generic;
using FDT.Backend.IDataModel;
using FDT.Backend.IExeHandler;
using FDT.Backend.OutputLayer;

namespace FDT.Backend.ExeHandler
{
    public class DamageAssessmentHandler: IRunnerHandler
    {
        public IFloodDamageDomain DataDomain { get; }
        public IExeWrapper ExeWrapper { get; }

        public DamageAssessmentHandler(IFloodDamageDomain floodDomain, IExeWrapper exeWrapper)
        {
            DataDomain = floodDomain ?? throw new ArgumentNullException(nameof(floodDomain));
            ExeWrapper = exeWrapper ?? throw new ArgumentNullException(nameof(exeWrapper));
        }

        public void Run()
        {
            if (DataDomain == null)
                throw new ArgumentNullException(nameof(DataDomain));
            IEnumerable<string> damageAssessmentFiles = XlsxDataWriter.WriteXlsxData(DataDomain);
            IExeWrapper fiatPythonWrapper = new FiatPythonWrapper(DataDomain.Paths.SystemPath);
            foreach (string damageAssessmentFile in damageAssessmentFiles)
            {
                fiatPythonWrapper.Run(damageAssessmentFile);
            }
        }
    }
}