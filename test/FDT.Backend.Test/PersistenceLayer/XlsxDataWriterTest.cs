using System;
using System.IO;
using System.Linq;
using FDT.Backend.DomainLayer.DataModel;
using FDT.Backend.DomainLayer.IDataModel;
using FDT.Backend.PersistenceLayer;
using FDT.Backend.PersistenceLayer.IFileObjectModel;
using NSubstitute;
using NUnit.Framework;

namespace FDT.Backend.Test.PersistenceLayer
{
    public class XlsxDataWriterTest
    {
        [Test]
        public void ConstructorTest()
        {
            var writer = new XlsxDataWriter();
            Assert.That(writer, Is.Not.Null);
            Assert.That(writer, Is.InstanceOf<IWriter>());
        }

        [Test]
        public void WriteXlsxDataWithWrongArgumentsThrowsException()
        {
            TestDelegate testAction = () => new XlsxDataWriter().WriteData(null).ToArray();
            Assert.That(testAction, Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void WriteXlsxDataTemplateNotFoundThrowsException()
        {
            var dummyDomainData = new FloodDamageDomain();
            Assert.That(Directory.Exists(dummyDomainData.Paths.DatabasePath), Is.False);
            TestDelegate testAction = () => new XlsxDataWriter().WriteData(dummyDomainData).ToArray();
            Assert.That(testAction, Throws.TypeOf<FileNotFoundException>());
        }

        [Test]
        public void WriteXlsxDataResultsPathCreatedIfDoesNotExist()
        {
            // Define test data.
            IFloodDamageDomain dummyDomainData = GetDummyDomain();
            string resultsPath = Path.Combine(dummyDomainData.Paths.RootPath, "dummyResultsDir");
            dummyDomainData.Paths.ResultsPath.Returns(resultsPath);
            if(Directory.Exists(dummyDomainData.Paths.ResultsPath))
                Directory.Delete(dummyDomainData.Paths.ResultsPath, true);

            // Define test action.
            TestDelegate testAction = () => new XlsxDataWriter().WriteData(dummyDomainData).ToArray();

            // Verify final expectations
            Assert.That(testAction, Throws.Nothing);
            Assert.That(Directory.Exists(dummyDomainData.Paths.ResultsPath), Is.True);
            if (Directory.Exists(dummyDomainData.Paths.ResultsPath))
                Directory.Delete(dummyDomainData.Paths.ResultsPath, true);
        }

        [Test]
        [TestCaseSource(typeof(PersistenceLayerTestData), nameof(PersistenceLayerTestData.InvalidIBasin))]
        public void TestWriteDataThrowsWhenInvalidIBasin(IBasin testCaseBasin, Type exceptionType, string exceptionMessage)
        {
            // 1. Prepare test data.
            IFloodDamageDomain testDomain = GetDummyDomain();
            testDomain.BasinData.Returns(testCaseBasin);
            string resultsPath = Path.Combine(testDomain.Paths.RootPath, "dummyResultsDir");
            testDomain.Paths.ResultsPath.Returns(resultsPath);
            if (Directory.Exists(testDomain.Paths.ResultsPath))
                Directory.Delete(testDomain.Paths.ResultsPath, true);

            // 2. Define test action.
            TestDelegate testAction = () => XlsDataWriteHelper.ValidateBasinData(testCaseBasin);

            // 3. Verify final expectations.
            Assert.That(testAction, Throws.TypeOf(exceptionType).With.Message.Contains(exceptionMessage));
            if (Directory.Exists(testDomain.Paths.ResultsPath))
                Directory.Delete(testDomain.Paths.ResultsPath, true);
        }

        [Test]
        public void WriteBasinCsvDataTest()
        {
            // Define initial expectations.
            var testDomain = GetDummyDomain();
            IBasin basinData = new BasinData()
            {
                BasinName = "Test Basin",
                Projection = "EPSG:42",
                Scenarios = new[]{
                    new ScenarioData()
                    {
                        ScenarioName = "Test Scenario A",
                        FloodMaps = new[]
                        {
                            new FloodMap() { Path = "dummy//Path//A"},
                            new FloodMap() {Path = "dummy//Path//B"}
                        }
                    },
                    new ScenarioData()
                    {
                        ScenarioName = "Test Scenario B",
                        FloodMaps = new []
                        {
                            new FloodMapWithReturnPeriod()
                            {
                                Path="dummy//Path//C",
                                ReturnPeriod = 42,
                            },
                            new FloodMapWithReturnPeriod()
                            {
                                Path="dummy//Path//D",
                                ReturnPeriod = 24,
                            }
                        }
                    }
                }
            };
            testDomain.BasinData.Returns(basinData);
            Assert.That(Directory.Exists(testDomain.Paths.RootPath));

            // Test Action
            IOutputData[] generatedFiles = null;
            TestDelegate testAction = () => generatedFiles = new XlsxDataWriter().WriteData(testDomain).ToArray();

            // Verify final expectations.
            Assert.That(testAction, Throws.Nothing);
            Assert.That(generatedFiles, Is.Not.Null.Or.Empty);
            Assert.That(generatedFiles.Length, Is.EqualTo(basinData.Scenarios.Count()));
            Assert.That(generatedFiles.All( gf => File.Exists(gf.ConfigurationFilePath)));
        }

        private IFloodDamageDomain GetDummyDomain()
        {
            var floodDamageDomain = Substitute.For<IFloodDamageDomain>();
            floodDamageDomain.BasinData = Substitute.For<IBasin>();
            floodDamageDomain.Paths = Substitute.For<IApplicationPaths>();
            var dummyScenario = Substitute.For<IScenario>();
            floodDamageDomain.BasinData.Scenarios.Returns(new[] {dummyScenario});

            string debugDir = TestHelper.AssemblyDirectory;
            string testDataDir = Path.Combine(debugDir, "TestData");
            string rootDir = Path.Combine(testDataDir, "TestRoot");
            Assert.That(Directory.Exists(rootDir));

            floodDamageDomain.BasinData.BasinName.Returns("ValidBasinName");
            floodDamageDomain.BasinData.Projection.Returns("ValidProjection");

            floodDamageDomain.Paths.RootPath = rootDir;
            floodDamageDomain.Paths.DatabasePath.Returns(Path.Combine(rootDir, "database"));
            floodDamageDomain.Paths.ResultsPath.Returns(Path.Combine(rootDir, "results"));

            dummyScenario.ScenarioName.Returns("A Scenario Name");

            return floodDamageDomain;
        }

    }
}