using System.Collections.Generic;
using FDT.Backend.DomainLayer.IDataModel;
using FDT.Backend.PersistenceLayer.IFileObjectModel;

namespace FDT.Backend.PersistenceLayer
{
    public interface IWriter
    {
        public IEnumerable<IOutputData> WriteData(IFloodDamageDomain domainData);
    }
}