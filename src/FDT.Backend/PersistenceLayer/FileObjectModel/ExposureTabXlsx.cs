using System;
using System.Collections.Generic;
using FIAT.Backend.DomainLayer.IDataModel;
using FIAT.Backend.PersistenceLayer.IFileObjectModel;

namespace FIAT.Backend.PersistenceLayer.FileObjectModel
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

            ExposureRowSingleEntry = new ExposureRowEntry(basin, exposureDirPath);
            RowEntries = new[] { ExposureRowSingleEntry };
        }
    }
}