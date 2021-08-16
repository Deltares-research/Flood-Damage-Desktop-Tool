using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ClosedXML.Excel;
using FDT.Backend.IDataModel;

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
            foreach (IScenario dataScenario in basinData.Scenarios)
            {
                string scenarioName = dataScenario.ScenarioName.Replace(" ", "_").ToLowerInvariant();
                string filePath = Path.Combine(outputDirectory, $"{scenarioName}_configuration.xlsx");
                using (var stream = File.Open(baseTemplate, FileMode.Open, FileAccess.Read))
                using (var workbook = new XLWorkbook(stream))
                {
                    SetHazardValues(workbook, dataScenario);
                    workbook.SaveAs(filePath);
                    yield return filePath;
                }
            }
            
        }

        private static void SetHazardValues(IXLWorkbook workBook, IScenario dataScenario)
        {
            IXLWorksheet hazardWorksheet;
            workBook.Worksheets.TryGetWorksheet("Hazard", out hazardWorksheet);
            hazardWorksheet.Cell(2, 1).InsertData(dataScenario.FloodMaps.AsEnumerable());
            hazardWorksheet.Columns().AdjustToContents();
        }
    }
}