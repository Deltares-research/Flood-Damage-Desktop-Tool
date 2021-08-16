using System;
using System.Data;
using System.IO;
using System.Text;
using ClosedXML.Excel;
using FDT.Backend.IDataModel;

namespace FDT.Backend.OutputLayer
{
    public static class XlsxDataWriter
    {
        public static void WriteXlsxData(string filePath, IBasin basinData)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentNullException(nameof(filePath));
            if (basinData == null)
                throw new ArgumentNullException(nameof(basinData));

            // Get the template.
            DataSet readTables = null;
            DirectoryInfo directoryInfo = Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.Parent;
            string baseTemplate = Path.Combine(directoryInfo?.FullName, "base_configuration_file.xlsx");
            if (!File.Exists(baseTemplate))
                throw new FileNotFoundException(baseTemplate);
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            using (var stream = File.Open(baseTemplate, FileMode.Open, FileAccess.Read))
            using (var workbook = new XLWorkbook(stream))
            {
                workbook.SaveAs(filePath);
            }
        }
    }
}