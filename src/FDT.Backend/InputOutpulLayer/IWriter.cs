using System.Collections.Generic;
using FDT.Backend.IDataModel;
using FDT.Backend.InputOutpulLayer.IFileObjectModel;

namespace FDT.Backend.InputOutpulLayer
{
    public interface IWriter
    {
        public IEnumerable<IOutputData> WriteData(IFloodDamageDomain domainData);
    }
}