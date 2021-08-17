using System.Collections.Generic;

namespace FDT.Backend.OutputLayer.IFileObjectModel
{
    public interface ITabXlsx
    {
        string TabName { get; }
        IEnumerable<IRowEntry> RowEntries { get; }
    }
}