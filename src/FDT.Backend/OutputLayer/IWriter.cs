using System.Collections.Generic;
using FDT.Backend.IDataModel;
using FDT.Backend.OutputLayer.IFileObjectModel;

namespace FDT.Backend.OutputLayer
{
    public interface IWriter
    {
        public IEnumerable<IOutputData> WriteData(IFloodDamageDomain domainData);
    }
}