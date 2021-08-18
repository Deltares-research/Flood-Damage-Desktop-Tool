using System.Collections.Generic;

namespace FDT.Backend.InputOutpulLayer.IFileObjectModel
{
    public interface ITabXlsx
    {
        string TabName { get; }
        IEnumerable<IRowEntry> RowEntries { get; }
    }
}