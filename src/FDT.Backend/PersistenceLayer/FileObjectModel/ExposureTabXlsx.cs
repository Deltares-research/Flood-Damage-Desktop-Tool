using System;
using System.Collections.Generic;
using FDT.Backend.DomainLayer.IDataModel;
using FDT.Backend.PersistenceLayer.IFileObjectModel;

namespace FDT.Backend.PersistenceLayer.FileObjectModel
{
    public class ExposureTabXlsx: ITabXlsx
    {
        private static readonly string _tabName = "Exposure";
        public string TabName => _tabName;
        public IEnumerable<IRowEntry> RowEntries { get; }
        public ExposureRowEntry ExposureRowSingleEntry { get; }
        public ExposureTabXlsx(IBasin basin, string exposureDirPath)
        {
            if (basin == null)
                throw new ArgumentNullException(nameof(basin));
            if (string.IsNullOrEmpty(exposureDirPath))
                throw new ArgumentNullException(nameof(exposureDirPath));

            ExposureRowSingleEntry = new ExposureRowEntry(basin.BasinName, exposureDirPath);
            RowEntries = new[] { ExposureRowSingleEntry };
        }
    }
}