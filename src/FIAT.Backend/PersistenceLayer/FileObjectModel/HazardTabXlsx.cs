using System;
using System.Collections.Generic;
using System.Linq;
using FIAT.Backend.DomainLayer.IDataModel;
using FIAT.Backend.PersistenceLayer.IFileObjectModel;

namespace FIAT.Backend.PersistenceLayer.FileObjectModel
{
    public class HazardTabXlsx : ITabXlsx
    {
        private static readonly string _tabName = "Hazard";
        public string TabName => _tabName;
        public IEnumerable<IRowEntry> RowEntries { get; }

        public HazardTabXlsx(IBasin basin, IEnumerable<IFloodMapBase> floodMaps)
        {
            if (basin == null)
                throw new ArgumentNullException(nameof(basin));
            if (floodMaps == null)
                throw new ArgumentNullException(nameof(floodMaps));
            RowEntries = floodMaps
                .Where( fm => !string.IsNullOrEmpty(fm.Path))
                .Select(fm => new HazardRowEntry(fm, basin.Projection));
        }
    }
}