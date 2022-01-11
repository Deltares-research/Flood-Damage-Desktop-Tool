using System;
using System.Collections.Generic;
using ClosedXML.Excel;
using FDT.Backend.DomainLayer.IDataModel;
using FDT.Backend.PersistenceLayer.IFileObjectModel;

namespace FDT.Backend.PersistenceLayer.FileObjectModel
{
    public class HazardRowEntry : IRowEntry
    {
        private string HazardFile { get; }
        private object ReturnPeriod { get; }
        private string CRS { get; }
        private string InundationReference { get; }
        public HazardRowEntry(IFloodMapBase floodMapBase, string basinProjection)
        {
            if (floodMapBase == null)
                throw new ArgumentNullException(nameof(floodMapBase));
            if (string.IsNullOrEmpty(basinProjection))
                throw new ArgumentNullException(nameof(basinProjection));
            
            HazardFile = floodMapBase.Path;
            ReturnPeriod = floodMapBase.GetReturnPeriod();
            CRS = basinProjection;
            InundationReference = GetInundationReference(floodMapBase.MapType);
        }

        public IEnumerable<object> GetOrderedColumns(IXLRow defaultRow)
        {
            if (defaultRow == null)
                throw new ArgumentNullException(nameof(defaultRow));
            return new[]
            {
                HazardFile,
                ReturnPeriod,
                CRS,
                InundationReference
            };
        }

        private string GetInundationReference(FloodMapType mapType)
        {
            return mapType switch
            {
                FloodMapType.WaterDepth => "DEM",
                FloodMapType.WaterLevel => "Datum",
                _ => throw new ArgumentException($"Unknown Flood Map type {mapType}")
            };
        }
    }
}