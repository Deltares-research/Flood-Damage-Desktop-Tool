using System;
using System.Collections;
using System.IO;
using FDT.Backend.PersistenceLayer.FileObjectModel;
using FDT.Backend.PersistenceLayer.IFileObjectModel;
using NUnit.Framework;

namespace FDT.Backend.Test.PersistenceLayer.FileObjectModel
{
    public class OutputDataTest
    {
        [Test]
        public void ConstructorTest()
        {
            var outputDataTest = new OutputData();
            Assert.That(outputDataTest, Is.Not.Null);
            Assert.That(outputDataTest, Is.InstanceOf<IOutputData>());
        }

        private static IOutputData GetTestOutputData(string configurationFilePath, string basinName, string scenarioName)
        {
            return new OutputData()
            {
                ConfigurationFilePath = configurationFilePath,
                BasinName = basinName,
                ScenarioName = scenarioName
            };
        }

        private static IEnumerable OutputDataInvalidParametersCases
        {
            get
            {
                const string aValidConfigurationFilePath = "AnyPath";
                const string aValidBasinName = "ABasinName";

                yield return new TestCaseData(GetTestOutputData(null, null, null), typeof(ArgumentNullException),
                    nameof(IOutputData.ConfigurationFilePath));
                yield return new TestCaseData(GetTestOutputData(string.Empty, null, null), typeof(ArgumentNullException),
                    nameof(IOutputData.ConfigurationFilePath));
                yield return new TestCaseData(GetTestOutputData(aValidConfigurationFilePath, null, null), typeof(ArgumentNullException),
                    nameof(IOutputData.BasinName));
                yield return new TestCaseData(GetTestOutputData(aValidConfigurationFilePath, string.Empty, null), typeof(ArgumentNullException),
                    nameof(IOutputData.BasinName));
                yield return new TestCaseData(GetTestOutputData(aValidConfigurationFilePath, aValidBasinName, null), typeof(ArgumentNullException),
                    nameof(IOutputData.ScenarioName));
                yield return new TestCaseData(GetTestOutputData(aValidConfigurationFilePath, aValidBasinName, string.Empty), typeof(ArgumentNullException),
                    nameof(IOutputData.ScenarioName));

            }
        }

        [Test]
        [TestCaseSource(nameof(OutputDataInvalidParametersCases))]
        public void ValidateParametersTestThrowsException(IOutputData outputData, Type exceptionType, string message)
        {
            TestDelegate testAction = () => outputData.ValidateParameters();
            Assert.That(testAction, Throws.TypeOf(exceptionType).With.Message.Contains(message));
        }
    }
}