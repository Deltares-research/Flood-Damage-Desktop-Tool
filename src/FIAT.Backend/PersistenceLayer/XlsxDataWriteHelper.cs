using System;
using System.Collections.Generic;
using System.Linq;
using ClosedXML.Excel;
using FIAT.Backend.DomainLayer.IDataModel;
using FIAT.Backend.Properties;

namespace FIAT.Backend.PersistenceLayer
{
    public static class XlsxDataWriteHelper
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

        public static void ValidateFloodDamageBasinData(this IFloodDamageBasin basinData)
        {
            if (basinData == null)
                throw new ArgumentNullException(nameof(basinData));
            if (string.IsNullOrEmpty(basinData.BasinName))
                throw new ArgumentNullException(nameof(basinData.BasinName));
            if (string.IsNullOrEmpty(basinData.Projection))
                throw new ArgumentNullException(nameof(basinData.Projection));
            if (basinData.Scenarios == null || !basinData.Scenarios.Any())
                throw new Exception(Resources.XlsDataWriteHelper_ValidateFloodDamageBasinData_No_valid_scenarios_were_provided_);
            if (basinData.Scenarios.Any(s => string.IsNullOrEmpty(s.ScenarioName)))
                throw new Exception(Resources.XlsDataWriteHelper_ValidateFloodDamageBasinData_All_selected_scenarios_should_contain_a_valid_name_);
            basinData.Scenarios.SelectMany( s => s.FloodMaps.OfType<IFloodMapWithReturnPeriod>()).ValidateFloodMapsWithReturnPeriod();
        }

        public static void ValidateFloodMapsWithReturnPeriod(this IEnumerable<IFloodMapWithReturnPeriod> floodMaps)
        {
            if (floodMaps == null)
                throw new ArgumentNullException(nameof(floodMaps));
            if (floodMaps.Any(fm => fm.ReturnPeriod <= 0))
                throw new Exception(Resources.XlsDataWriteHelper_ValidateFloodMapsWithReturnPeriod_All_selected_Flood_Maps_with_return_period_should_be_greater_than_0_);
        }
    }
}