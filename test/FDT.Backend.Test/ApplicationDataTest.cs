using System;
using System.IO;
using System.Linq;
using FDT.Backend.DataModel;
using FDT.Backend.IDataModel;
using FDT.Backend.OutputLayer;
using NUnit.Framework;

namespace FDT.Backend.Test
{
    public class Tests
    {

        [Test]
        public void WriteBasinCsvDataTest()
        {
            // Define initial expectations.
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
            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            string testDirectory = Path.Combine(projectDirectory, "TestData", "TestRoot");
            var applicationPaths = new ApplicationPaths()
            {
                RootPath = testDirectory
            };
            var domain = new FloodDamageDomain()
            {
                BasinData = basinData,
                Paths = applicationPaths
            };
            Assert.That(Directory.Exists(applicationPaths.RootPath));

            // Test Action
            string[] generatedFiles = {};
            TestDelegate testAction = () => generatedFiles = XlsxDataWriter.WriteXlsxData(domain).ToArray();

            // Verify final expectations.
            Assert.That(testAction, Throws.Nothing);
            Assert.That(generatedFiles, Is.Not.Null.Or.Empty);
            Assert.That(generatedFiles.All(File.Exists));
        }

    }
}