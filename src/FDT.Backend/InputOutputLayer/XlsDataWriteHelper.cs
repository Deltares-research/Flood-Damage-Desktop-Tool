using System;
using ClosedXML.Excel;

namespace FDT.Backend.InputOutputLayer
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
    }
}