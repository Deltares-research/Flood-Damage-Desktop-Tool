using System;
using System.Collections.Generic;
using ClosedXML.Excel;
using FDT.Backend.PersistenceLayer.IFileObjectModel;

namespace FDT.Backend.PersistenceLayer.FileObjectModel
{
    public class ExposureRowEntry : IRowEntry
    {
        private const string ExposureFileName = "exposure.csv";
        private string ExposureRelativePath;

        public ExposureRowEntry(string selectedBasin)
        {
            if (string.IsNullOrEmpty(selectedBasin))
                throw new ArgumentNullException(nameof(selectedBasin)); 
            ExposureRelativePath = $"Exposure\\{selectedBasin}\\{ExposureFileName}";
        }

        public IEnumerable<object> GetOrderedColumns(IXLRow defaultRow)
        {
            return new []{ ExposureRelativePath, string.Empty};
        }
    }
}