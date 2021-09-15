﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ClosedXML.Excel;
using FDT.Backend.DomainLayer.IDataModel;
using FDT.Backend.PersistenceLayer.FileObjectModel;
using FDT.Backend.PersistenceLayer.IFileObjectModel;

namespace FDT.Backend.PersistenceLayer
{
    public class XlsxDataWriter : IWriter
    {
        public IEnumerable<IOutputData> WriteData(IFloodDamageDomain domainData)
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

            domainData.BasinData.ValidateBasinData();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            foreach (IScenario scenarioData in domainData.BasinData.Scenarios)
            {
                string scenarioName = scenarioData.ScenarioName.Replace(" ", "_").ToLowerInvariant();
                string filePath = Path.Combine(domainData.Paths.ResultsPath, $"{scenarioName}_configuration.xlsx");
                ITabXlsx[] tabs = {
                    new SettingsTabXlsx(domainData.BasinData, scenarioData.ScenarioName),
                    new HazardTabXlsx(domainData.BasinData, scenarioData.FloodMaps),
                    new ExposureTabXlsx(domainData.BasinData)
                };
                using (var stream = File.Open(baseTemplate, FileMode.Open, FileAccess.Read))
                using (var workbook = new XLWorkbook(stream))
                {
                    foreach (ITabXlsx tabXlsx in tabs)
                    {
                        IXLWorksheet settingsWorksheet = workbook.GetWorksheet(tabXlsx.TabName);
                        // First column contains just the headers, the second one the default values.
                        IXLRow defaultRow = settingsWorksheet.Row(2);
                        settingsWorksheet
                            .Cell(2, 1)
                            .InsertData(tabXlsx.RowEntries.Select( re => re.GetOrderedColumns(defaultRow)))
                            .Style.Fill.SetBackgroundColor(XLWorkbook.DefaultStyle.Fill.BackgroundColor);
                        settingsWorksheet.Columns().AdjustToContents();
                    }
                    workbook.SaveAs(filePath);
                    yield return new OutputData()
                    {
                        ConfigurationFilePath = filePath,
                        BasinName = domainData.BasinData.BasinName,
                        ScenarioName = scenarioName
                    };
                    stream.Flush();
                }
            }
        }
    }
}