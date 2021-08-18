using System.Collections.Generic;
using FDT.Backend.IDataModel;
using FDT.Backend.InputOutputLayer.IFileObjectModel;

namespace FDT.Backend.InputOutputLayer
{
    public interface IWriter
    {
        public IEnumerable<IOutputData> WriteData(IFloodDamageDomain domainData);
    }
}