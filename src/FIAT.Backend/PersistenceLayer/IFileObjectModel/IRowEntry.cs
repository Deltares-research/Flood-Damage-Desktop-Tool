using System.Collections.Generic;
using ClosedXML.Excel;

namespace FIAT.Backend.PersistenceLayer.IFileObjectModel
{
    public interface IRowEntry
    {
        IEnumerable<object> GetOrderedColumns(IXLRow defaultRow);
    }
}