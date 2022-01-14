using System.Collections.Generic;

namespace FDT.Backend.DomainLayer.IDataModel
{
    public interface IFloodDamageBasin : IBasin
    {
        IEnumerable<IScenario> Scenarios { get; set; }

    }
}