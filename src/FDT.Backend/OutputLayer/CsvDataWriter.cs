using System;
using System.Globalization;
using System.IO;
using CsvHelper;
using FDT.Backend.IDataModel;

namespace FDT.Backend.OutputLayer
{
    public static class CsvDataWriter
    {
        public static void WriteCsvData(string filePath, IBasin basinData)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentNullException(nameof(filePath));
            if (basinData == null)
                throw new ArgumentNullException(nameof(basinData));

            using (var writer = new StreamWriter(filePath))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(new[] {basinData});
            }
        }
    }
}