using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            };
            
            // Test Action
            string[] generatedFiles = new string[]{};
            TestDelegate testAction = () => generatedFiles = XlsxDataWriter.WriteXlsxData(Environment.CurrentDirectory, basinData).ToArray();

            // Verify final expectations.
            Assert.That(testAction, Throws.Nothing);
            Assert.That(generatedFiles, Is.Not.Null.Or.Empty);
            Assert.That(generatedFiles.All(cf => File.Exists(cf)));
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