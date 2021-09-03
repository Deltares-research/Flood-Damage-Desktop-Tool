using System;
using System.Collections.Generic;
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

        public IEnumerable<object> GetOrderedColumns()
        {
            return new []{ExposureRelativePath};
        }
    }
}