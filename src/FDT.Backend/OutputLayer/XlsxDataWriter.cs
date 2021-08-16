using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ClosedXML.Excel;
using FDT.Backend.IDataModel;
using FDT.Backend.OutputLayer.FileObjectModel;

namespace FDT.Backend.OutputLayer
{
    public static class XlsxDataWriter
    {
        public static IEnumerable<string> WriteXlsxData(string outputDirectory, IBasin basinData)
        {
            if (string.IsNullOrEmpty(outputDirectory))
                throw new ArgumentNullException(nameof(outputDirectory));
            if (basinData == null)
                throw new ArgumentNullException(nameof(basinData));

            // Get the template.
            DirectoryInfo directoryInfo = Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.Parent;
            const string baseConfigurationFileXlsx = "base_configuration_file.xlsx";
            string baseTemplate = Path.Combine(directoryInfo?.FullName!, baseConfigurationFileXlsx);
            if (!File.Exists(baseTemplate))
                throw new FileNotFoundException(baseTemplate);
            
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            foreach (IScenario scenarioData in basinData.Scenarios)
            {
                string scenarioName = scenarioData.ScenarioName.Replace(" ", "_").ToLowerInvariant();
                string filePath = Path.Combine(outputDirectory, $"{scenarioName}_configuration.xlsx");
                ITabXlsx[] tabs = {
                    new SettingsTabXlsx(basinData, scenarioData.ScenarioName),
                    new HazardTabXlsx(basinData, scenarioData.FloodMaps)
                };
                using (var stream = File.Open(baseTemplate, FileMode.Open, FileAccess.Read))
                using (var workbook = new XLWorkbook(stream))
                {
                    foreach (ITabXlsx tabXlsx in tabs)
                    {
                        IXLWorksheet settingsWorksheet = XlsDataWriteHelper.GetWorksheet(workbook, tabXlsx.TabName);
                        settingsWorksheet.Cell(2, 1).InsertData(tabXlsx.RowEntries.AsEnumerable()).Style.Fill.SetBackgroundColor(XLWorkbook.DefaultStyle.Fill.BackgroundColor);
                        settingsWorksheet.Columns().AdjustToContents();
                    }
                    workbook.SaveAs(filePath);
                    yield return filePath;
                }
            }
            
        }
    }
}