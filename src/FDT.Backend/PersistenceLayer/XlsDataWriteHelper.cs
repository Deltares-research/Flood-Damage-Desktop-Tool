﻿using System;
using System.Collections.Generic;
using System.Linq;
using ClosedXML.Excel;
using FDT.Backend.DomainLayer.IDataModel;

namespace FDT.Backend.PersistenceLayer
{
    public static class XlsDataWriteHelper
    {
        public static IXLWorksheet GetWorksheet(this IXLWorkbook workBook, string tabName)
        {
            if (workBook == null)
                throw new ArgumentNullException(nameof(workBook));
            if (string.IsNullOrEmpty(tabName))
                throw new ArgumentNullException(nameof(tabName));
            IXLWorksheet hazardWorksheet;
            workBook.Worksheets.TryGetWorksheet(tabName, out hazardWorksheet);
            return hazardWorksheet;
        }

        public static void ValidateBasinData(this IBasin basinData)
        {
            if (basinData == null)
                throw new ArgumentNullException(nameof(basinData));
            if (string.IsNullOrEmpty(basinData.BasinName))
                throw new ArgumentNullException(nameof(basinData.BasinName));
            if (string.IsNullOrEmpty(basinData.Projection))
                throw new ArgumentNullException(nameof(basinData.Projection));
            if (basinData.Scenarios == null || !basinData.Scenarios.Any())
                throw new Exception("No valid scenarios were provided.");
            if (basinData.Scenarios.Any(s => string.IsNullOrEmpty(s.ScenarioName)))
                throw new Exception("All selected scenarios should contain a valid name.");
            basinData.Scenarios.SelectMany( s => s.FloodMaps.OfType<IFloodMapWithReturnPeriod>()).ValidateFloodMapsWithReturnPeriod();
        }

        public static void ValidateFloodMapsWithReturnPeriod(this IEnumerable<IFloodMapWithReturnPeriod> floodMaps)
        {
            if (floodMaps == null)
                throw new ArgumentNullException(nameof(floodMaps));
            if (floodMaps.Any(fm => fm.ReturnPeriod <= 0))
                throw new Exception("All selected Flood Maps with return period should be greater than 0.");
        }
    }
}