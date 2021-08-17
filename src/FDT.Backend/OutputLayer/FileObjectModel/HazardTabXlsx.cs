﻿using System;
using System.Collections.Generic;
using System.Linq;
using FDT.Backend.IDataModel;
using FDT.Backend.OutputLayer.IFileObjectModel;

namespace FDT.Backend.OutputLayer.FileObjectModel
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