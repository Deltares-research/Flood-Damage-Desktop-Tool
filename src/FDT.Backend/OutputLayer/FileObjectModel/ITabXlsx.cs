using System.Collections.Generic;

namespace FDT.Backend.OutputLayer.FileObjectModel
{
    public interface ITabXlsx
    {
        string TabName { get; }
        IEnumerable<IRowEntry> RowEntries { get; }
    }
}