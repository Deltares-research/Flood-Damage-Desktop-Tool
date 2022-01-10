using System;
using System.Collections.Generic;
using ClosedXML.Excel;
using FDT.Backend.PersistenceLayer.IFileObjectModel;
using Path = System.IO.Path;

namespace FDT.Backend.PersistenceLayer.FileObjectModel
{
    public class ExposureRowEntry : IRowEntry
    {
        private const string ExposureFileName = "exposure.csv";
        private readonly string _exposureRelativePath;

        public ExposureRowEntry(string selectedBasin, string exposurePath)
        {
            if (string.IsNullOrEmpty(selectedBasin))
                throw new ArgumentNullException(nameof(selectedBasin));
            if (string.IsNullOrEmpty(exposurePath))
                throw new ArgumentNullException(nameof(exposurePath));
            _exposureRelativePath = Path.Combine(exposurePath, selectedBasin, ExposureFileName);
        }

        public IEnumerable<object> GetOrderedColumns(IXLRow defaultRow)
        {
            return new []{ _exposureRelativePath, string.Empty};
        }
    }
}