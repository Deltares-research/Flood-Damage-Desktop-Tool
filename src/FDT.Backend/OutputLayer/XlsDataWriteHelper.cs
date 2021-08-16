using ClosedXML.Excel;

namespace FDT.Backend.OutputLayer
{
    public static class XlsDataWriteHelper
    {
        public static IXLWorksheet GetWorksheet(IXLWorkbook workBook, string tabName)
        {
            IXLWorksheet hazardWorksheet;
            workBook.Worksheets.TryGetWorksheet(tabName, out hazardWorksheet);
            return hazardWorksheet;
        }
    }
}