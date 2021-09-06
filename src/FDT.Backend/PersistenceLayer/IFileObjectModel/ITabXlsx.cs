using System.Collections.Generic;

namespace FDT.Backend.PersistenceLayer.IFileObjectModel
{
    public interface ITabXlsx
    {
        string TabName { get; }
        IEnumerable<IRowEntry> RowEntries { get; }
    }
}