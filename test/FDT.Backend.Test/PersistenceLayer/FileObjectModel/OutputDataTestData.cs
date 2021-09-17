using System;
using System.Collections;
using System.IO;
using FDT.Backend.PersistenceLayer.FileObjectModel;
using FDT.Backend.PersistenceLayer.IFileObjectModel;
using NUnit.Framework;

namespace FDT.Backend.Test.PersistenceLayer.FileObjectModel
{
    public class OutputDataTestData
    {
        private static IOutputData GetTestOutputData(string configurationFilePath, string basinName, string scenarioName)
        {
            return new OutputData()
            {
                ConfigurationFilePath = configurationFilePath,
                BasinName = basinName,
                ScenarioName = scenarioName
            };
        }

        public static IEnumerable OutputDataInvalidParametersCases
        {
            get
            {
                const string anInvalidPath = "AnyPath";
                const string aValidBasinName = "ABasinName";
                const string aValidScenarioName = "AValidScenarioName";
                yield return new TestCaseData(null, typeof(ArgumentNullException), "outputData");
                yield return new TestCaseData(GetTestOutputData(null, null, null), typeof(ArgumentNullException),
                    nameof(IOutputData.BasinName));
                yield return new TestCaseData(GetTestOutputData(null, string.Empty, null), typeof(ArgumentNullException),
                    nameof(IOutputData.BasinName));
                yield return new TestCaseData(GetTestOutputData(null, aValidBasinName, null), typeof(ArgumentNullException),
                    nameof(IOutputData.ScenarioName));
                yield return new TestCaseData(GetTestOutputData(null, aValidBasinName, string.Empty), typeof(ArgumentNullException),
                    nameof(IOutputData.ScenarioName));
                yield return new TestCaseData(GetTestOutputData(null, aValidBasinName, aValidScenarioName), typeof(ArgumentNullException),
                    nameof(IOutputData.ConfigurationFilePath));
                yield return new TestCaseData(GetTestOutputData(string.Empty, aValidBasinName, aValidScenarioName), typeof(ArgumentNullException),
                    nameof(IOutputData.ConfigurationFilePath));
                yield return new TestCaseData(GetTestOutputData(anInvalidPath, aValidBasinName, aValidScenarioName), typeof(FileNotFoundException),
                    anInvalidPath);
            }
        }
    }
}