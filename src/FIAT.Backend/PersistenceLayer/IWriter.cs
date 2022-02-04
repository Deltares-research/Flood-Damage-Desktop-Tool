using System.Collections.Generic;
using FIAT.Backend.DomainLayer.IDataModel;
using FIAT.Backend.PersistenceLayer.IFileObjectModel;

namespace FIAT.Backend.PersistenceLayer
{
    public interface IWriter
    {
        public IEnumerable<IOutputData> WriteData(IFloodDamageDomain domainData);
    }
}