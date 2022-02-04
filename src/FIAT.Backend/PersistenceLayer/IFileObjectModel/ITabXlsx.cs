using System.Collections.Generic;

namespace FIAT.Backend.PersistenceLayer.IFileObjectModel
{
    public interface ITabXlsx
    {
        string TabName { get; }
        IEnumerable<IRowEntry> RowEntries { get; }
    }
}