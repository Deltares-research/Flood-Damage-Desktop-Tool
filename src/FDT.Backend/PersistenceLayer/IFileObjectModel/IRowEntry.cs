using System.Collections.Generic;

namespace FDT.Backend.PersistenceLayer.IFileObjectModel
{
    public interface IRowEntry
    {
        IEnumerable<object> GetOrderedColumns();
    }
}