using System;
using System.IO;
using FDT.Backend.ExeHandler;
using FDT.Backend.InputOutputLayer.FileObjectModel;
using FDT.Backend.InputOutputLayer.IFileObjectModel;
using NUnit.Framework;

namespace FDT.Backend.Test.InputOutputLayer.FileObjectModel
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

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void ValidateParametersThrowsExceptionWhenNullOrEmptyConfigurationFilePath(string filePath)
        {
            IOutputData outputData = new OutputData()
            {
                ConfigurationFilePath = filePath
            };
            TestDelegate testAction = () => new FiatPythonWrapper().ValidateRun(outputData);
            Assert.That(testAction, Throws.Exception.TypeOf<ArgumentNullException>().With.Message.Contains(nameof(IOutputData.ConfigurationFilePath)));
        }

        [Test]
        public void ValidateParametersThrowsFileNotFoundExceptionWhenConfigurationFilePathDoesNotExist()
        {
            const string filePath = "this\\path\\does\\not\\exist";
            IOutputData outputData = new OutputData()
            {
                ConfigurationFilePath = filePath
            };
            TestDelegate testAction = () => new FiatPythonWrapper().ValidateRun(outputData);
            Assert.That(testAction, Throws.Exception.TypeOf<FileNotFoundException>().With.Message.Contains(filePath));
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void ValidateParametersThrowsArgumentNullExceptionWhenBasinNameNullOrEmpty(string basinName)
        {
            IOutputData outputData = new OutputData()
            {
                ConfigurationFilePath = TestHelper.TestConfigurationFile,
                BasinName = basinName
            };
            TestDelegate testAction = () => new FiatPythonWrapper().ValidateRun(outputData);
            Assert.That(testAction, Throws.Exception.TypeOf<ArgumentNullException>().With.Message.Contains(nameof(IOutputData.BasinName)));
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void ValidateParametersThrowsArgumentNullExceptionWhenScenarioNameNullOrEmpty(string scenarioName)
        {
            IOutputData outputData = new OutputData()
            {
                ConfigurationFilePath = TestHelper.TestConfigurationFile,
                BasinName = "ABasinName",
                ScenarioName = scenarioName
            };
            TestDelegate testAction = () => new FiatPythonWrapper().ValidateRun(outputData);
            Assert.That(testAction, Throws.Exception.TypeOf<ArgumentNullException>().With.Message.Contains(nameof(IOutputData.ScenarioName)));
        }
    }
}