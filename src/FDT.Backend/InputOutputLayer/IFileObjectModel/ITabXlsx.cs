using System.Collections.Generic;

namespace FDT.Backend.InputOutputLayer.IFileObjectModel
{
    public interface ITabXlsx
    {
        string TabName { get; }
        IEnumerable<IRowEntry> RowEntries { get; }
    }
}