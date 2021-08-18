using System;
using System.IO;
using System.Linq;
using System.Reflection;
using FDT.Backend.DataModel;
using FDT.Backend.IDataModel;
using FDT.Backend.OutputLayer;
using FDT.Backend.OutputLayer.IFileObjectModel;
using NSubstitute;
using NUnit.Framework;

namespace FDT.Backend.Test.OutputLayer
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
            var resultsPath = Path.Combine(dummyDomainData.Paths.RootPath, "dummyResultsDir");
            dummyDomainData.Paths.ResultsPath.Returns(resultsPath);
            if(Directory.Exists(dummyDomainData.Paths.ResultsPath))
                Directory.Delete(dummyDomainData.Paths.ResultsPath);

            // Define test action.
            TestDelegate testAction = () => new XlsxDataWriter().WriteData(dummyDomainData).ToArray();

            // Verify final expectations
            Assert.That(testAction, Throws.Nothing);
            Assert.That(Directory.Exists(dummyDomainData.Paths.ResultsPath), Is.True);
            Directory.Delete(resultsPath);
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
                            new FloodMapBaseWithReturnPeriod()
                            {
                                Path="dummy//Path//C",
                                ReturnPeriod = 42,
                            },
                            new FloodMapBaseWithReturnPeriod()
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
            string debugDir = TestHelper.AssemblyDirectory;
            string testDataDir = Path.Combine(debugDir, "TestData");
            string rootDir = Path.Combine(testDataDir, "TestRoot");
            Assert.That(Directory.Exists(rootDir));
            floodDamageDomain.Paths.RootPath = rootDir;
            floodDamageDomain.Paths.DatabasePath.Returns(Path.Combine(rootDir, "database"));
            floodDamageDomain.Paths.ResultsPath.Returns(Path.Combine(rootDir, "results"));

            return floodDamageDomain;
        }
    }
}