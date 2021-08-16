using System;
using System.IO;
using FDT.Backend.DataModel;
using FDT.Backend.IDataModel;
using FDT.Backend.OutputLayer;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace FDT.Backend.Test
{
    public class Tests
    {

        [Test]
        public void WriteBasinCsvDataTest()
        {
            // Define initial expectations.
            string csvTestName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ".xlsx";
            string filePath = Path.Combine(Environment.CurrentDirectory, csvTestName);
            IBasin basinData = new BasinData();
            basinData.NameScenario = ScenarioType.Event;
            basinData.Scenarios = new[]
            {
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
            };
            
            // Test Action
            TestDelegate testAction = () => XlsxDataWriter.WriteXlsxData(filePath, basinData);

            // Verify final expectations.
            Assert.That(testAction, Throws.Nothing);
            Assert.That(File.Exists(filePath));
            try
            {
                // File.Delete(filePath);
            }
            catch (Exception e)
            {
                
                throw;
            }
        }

    }
}