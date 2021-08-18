﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ClosedXML.Excel;
using FDT.Backend.IDataModel;
using FDT.Backend.OutputLayer.FileObjectModel;
using FDT.Backend.OutputLayer.IFileObjectModel;

namespace FDT.Backend.OutputLayer
{
    public class XlsxDataWriter : IWriter
    {
        public IEnumerable<string> WriteData(IFloodDamageDomain domainData)
        {
            if (domainData == null)
                throw new ArgumentNullException(nameof(domainData));

            // Get the template.
            const string baseConfigurationFileXlsx = "base_configuration_file.xlsx";
            string baseTemplate = Path.Combine(domainData.Paths.DatabasePath, baseConfigurationFileXlsx);
            if (!File.Exists(baseTemplate))
                throw new FileNotFoundException(baseTemplate);

            if (!Directory.Exists(domainData.Paths.ResultsPath))
                Directory.CreateDirectory(domainData.Paths.ResultsPath);

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            foreach (IScenario scenarioData in domainData.BasinData.Scenarios)
            {
                string scenarioName = scenarioData.ScenarioName.Replace(" ", "_").ToLowerInvariant();
                string filePath = Path.Combine(domainData.Paths.ResultsPath, $"{scenarioName}_configuration.xlsx");
                ITabXlsx[] tabs = {
                    new SettingsTabXlsx(domainData.BasinData, scenarioData.ScenarioName),
                    new HazardTabXlsx(domainData.BasinData, scenarioData.FloodMaps)
                };
                using (var stream = File.Open(baseTemplate, FileMode.Open, FileAccess.Read))
                using (var workbook = new XLWorkbook(stream))
                {
                    foreach (ITabXlsx tabXlsx in tabs)
                    {
                        IXLWorksheet settingsWorksheet = workbook.GetWorksheet(tabXlsx.TabName);
                        settingsWorksheet.Cell(2, 1).InsertData(tabXlsx.RowEntries.AsEnumerable()).Style.Fill.SetBackgroundColor(XLWorkbook.DefaultStyle.Fill.BackgroundColor);
                        settingsWorksheet.Columns().AdjustToContents();
                    }
                    workbook.SaveAs(filePath);
                    yield return filePath;
                    stream.Flush();
                }
            }
        }
    }
}