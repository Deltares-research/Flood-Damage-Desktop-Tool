using System.Collections.Generic;
using ClosedXML.Excel;

namespace FDT.Backend.PersistenceLayer.IFileObjectModel
{
    public interface IRowEntry
    {
        IEnumerable<object> GetOrderedColumns(IXLRow defaultRow);
    }
}