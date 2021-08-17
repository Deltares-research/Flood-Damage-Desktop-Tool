using System;
using System.IO;
using System.Linq;
using System.Reflection;
using FDT.Backend.DataModel;
using FDT.Backend.IDataModel;
using FDT.Backend.OutputLayer;
using NSubstitute;
using NUnit.Framework;

namespace FDT.Backend.Test.OutputLayer
{
    public class XlsxDataWriterTest
    {
        [Test]
        public void WriteXlsxDataWithWrongArgumentsThrowsException()
        {
            TestDelegate testAction = () => XlsxDataWriter.WriteXlsxData(null);
            Assert.That(testAction, Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void WriteXlsxDataTemplateNotFoundThrowsException()
        {
            var dummyDomainData = new FloodDamageDomain();
            Assert.That(Directory.Exists(dummyDomainData.Paths.DatabasePath), Is.False);
            TestDelegate testAction = () => XlsxDataWriter.WriteXlsxData(dummyDomainData);
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
            TestDelegate testAction = () => XlsxDataWriter.WriteXlsxData(dummyDomainData);

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
            string[] generatedFiles = { };
            TestDelegate testAction = () => generatedFiles = XlsxDataWriter.WriteXlsxData(testDomain).ToArray();

            // Verify final expectations.
            Assert.That(testAction, Throws.Nothing);
            Assert.That(generatedFiles, Is.Not.Null.Or.Empty);
            Assert.That(generatedFiles.Length, Is.EqualTo(basinData.Scenarios.Count()));
            Assert.That(generatedFiles.All(File.Exists));
        }

        private IFloodDamageDomain GetDummyDomain()
        {
            var floodDamageDomain = Substitute.For<IFloodDamageDomain>();
            floodDamageDomain.BasinData = Substitute.For<IBasin>();
            floodDamageDomain.Paths = Substitute.For<IApplicationPaths>();
            string debugDir = AssemblyDirectory;
            string testDataDir = Path.Combine(debugDir, "TestData");
            string rootDir = Path.Combine(testDataDir, "TestRoot");
            Assert.That(Directory.Exists(rootDir));
            floodDamageDomain.Paths.RootPath = rootDir;
            floodDamageDomain.Paths.DatabasePath.Returns(Path.Combine(rootDir, "database"));
            floodDamageDomain.Paths.ResultsPath.Returns(Path.Combine(rootDir, "results"));

            return floodDamageDomain;
        }

        /// <summary>
        /// https://stackoverflow.com/questions/52797/how-do-i-get-the-path-of-the-assembly-the-code-is-in
        /// </summary>
        private static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }
    }
}