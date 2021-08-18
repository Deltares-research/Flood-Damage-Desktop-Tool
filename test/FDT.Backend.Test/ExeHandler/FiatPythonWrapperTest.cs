using System;
using System.IO;
using FDT.Backend.ExeHandler;
using FDT.Backend.IExeHandler;
using FDT.Backend.OutputLayer.IFileObjectModel;
using NSubstitute;
using NSubstitute.Extensions;
using NUnit.Framework;

namespace FDT.Backend.Test.ExeHandler
{
    public class FiatPythonWrapperTest
    {
        [Test]
        public void ConstructorTest()
        {
            // Define test data
            FiatPythonWrapper testWrapper = null;

            // Define test action
            TestDelegate testDelegate = () => testWrapper = new FiatPythonWrapper();

            // Verify final expectations.
            Assert.That(testDelegate, Throws.Nothing);
            Assert.That(testWrapper, Is.Not.Null);
            Assert.That(testWrapper, Is.InstanceOf<IExeWrapper>());
            Assert.That(testWrapper.ExeDirectory, Is.Not.Null.Or.Empty);
            Assert.That(Path.GetFileName(testWrapper.ExeFilePath), Is.EqualTo("fiat_objects.exe"));
        }

        [Test]
        public void RunThrowsArgumentNullExceptionWhenOutputDataIsNull()
        {
            TestDelegate testAction = () => new FiatPythonWrapper().Run(null);
            Assert.That(testAction,
                Throws.Exception.TypeOf<ArgumentNullException>().With.Message.Contains("outputData"));
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void RunThrowsArgumentNullExceptionWhenFilePathNullOrEmpty(string filePath)
        {
            var outputData = Substitute.For<IOutputData>();
            outputData.ConfigurationFilePath.Returns(filePath);
            TestDelegate testAction = () => new FiatPythonWrapper().Run(outputData);
            Assert.That(testAction,
                Throws.Exception.TypeOf<ArgumentNullException>().With.Message.Contains("ConfigurationFilePath"));
        }

        [Test]
        public void RunThrowsFileNotFoundExceptionWhenFilePathDoesNotExist()
        {
            const string testFilePath = "Non//Existing//Path";
            var outputData = Substitute.For<IOutputData>();
            outputData.ConfigurationFilePath.Returns(testFilePath);
            TestDelegate testAction = () => new FiatPythonWrapper().Run(outputData);
            Assert.That(testAction, Throws.Exception.TypeOf<FileNotFoundException>());
        }

        [Test]
        public void ValidateRunThrowsExceptionWithNullOutputData()
        {
            TestDelegate testAction = () => new FiatPythonWrapper().ValidateRun(null);
            Assert.That(testAction, Throws.Exception.TypeOf<ArgumentNullException>().With.Message.Contains("outputData"));
        }
    }

    public class FiatPythonWrapperValidateRunAcceptanceTest
    {
        private IOutputData _testOutputData;
        private string _basinDir;
        private string _scenarioDir;
        private string _copyConfigFile;
        private string resultsDir => Path.Combine(TestHelper.TestRootDirectory, "results");

        [SetUp]
        public void SetUpTestDirectory()
        {
            // Expected directory structure:
            // Root
            // |-database\\TestConfigurationFile.xlsx
            // |-results\\C42\\Scenario42
            const string basinName = "C42";
            const string scenarioName = "Scenario42";
            Assert.That(Directory.Exists(resultsDir));

            _copyConfigFile = Path.Combine(resultsDir, Path.GetFileName(TestHelper.TestConfigurationFile));
            if (!File.Exists(_copyConfigFile))
                File.Copy(TestHelper.TestConfigurationFile, _copyConfigFile);

            _basinDir = Path.Combine(resultsDir, basinName);
            _scenarioDir = Path.Combine(_basinDir, scenarioName);

            if (Directory.Exists(_scenarioDir))
                // Cleanup everything to avoid issues.
                Directory.Delete(_basinDir, true);
            Directory.CreateDirectory(_scenarioDir);


            _testOutputData = Substitute.For<IOutputData>();
            _testOutputData.ConfigurationFilePath.Returns(_copyConfigFile);
            _testOutputData.BasinName.Returns(basinName);
            _testOutputData.ScenarioName.Returns(scenarioName);
        }

        [TearDown]
        public void CleanUpTestDirectory()
        {
            _testOutputData = null;
            Directory.Delete(_scenarioDir, true);
            File.Delete(_copyConfigFile);
        }

        [Test]
        public void ValidateRunFailsWhenBasinDirDoesNotExist()
        {
            // 1. Define test data.
            var pythonWrapper = new FiatPythonWrapper();
            bool isValidRun = false;
            _testOutputData.BasinName.Returns("ThisDirDoesNotExist");
            
            // 2. Define test delegate.
            TestDelegate testAction = () => isValidRun = pythonWrapper.ValidateRun(_testOutputData);

            // 3. Verify final expectations.
            Assert.That(testAction, Throws.Nothing);
            Assert.That(isValidRun, Is.False);
        }

        [Test]
        public void ValidateRunFailsWhenResultsNotGenerated()
        {
            // 1. Define test data.
            var pythonWrapper = new FiatPythonWrapper();
            bool isValidRun = false;

            // 2. Define test delegate.
            TestDelegate testAction = () => isValidRun = pythonWrapper.ValidateRun(_testOutputData);

            // 3. Verify final expectations.
            Assert.That(testAction, Throws.Nothing);
            Assert.That(isValidRun, Is.False);
        }

        [Test]
        public void ValidateRunSucceedsAndMovesConfigurationFileWhenResultsNotGenerated()
        {
            // 1. Define test data.
            var pythonWrapper = new FiatPythonWrapper();
            bool isValidRun = false;
            
            // Just copy a file to make the test detect something.
            File.Copy(_copyConfigFile, Path.Combine(_scenarioDir, "dummy_results.csv"));
            Assert.That(Directory.GetFiles(_scenarioDir), Is.Not.Empty);

            // 2. Define test delegate.
            TestDelegate testAction = () => isValidRun = pythonWrapper.ValidateRun(_testOutputData);
            
            // 3. Verify final expectations.
            Assert.That(testAction, Throws.Nothing);
            Assert.That(isValidRun, Is.True);
            Assert.That(File.Exists(_copyConfigFile), Is.Not.True);
            string movedConfigFilePath = Path.Combine(_basinDir, Path.GetFileName(_copyConfigFile));
            Assert.That(File.Exists(movedConfigFilePath));
        }

    }
}