using System.Collections.Generic;
using FDT.Backend.IDataModel;

namespace FDT.Backend.OutputLayer
{
    public interface IWriter
    {
        public IEnumerable<string> WriteData(IFloodDamageDomain domainData);
    }
}