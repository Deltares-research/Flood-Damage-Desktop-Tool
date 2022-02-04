using System.Collections.Generic;

namespace FIAT.Backend.DomainLayer.IDataModel
{
    public interface IFloodDamageBasin : IBasin
    {
        IEnumerable<IScenario> Scenarios { get; set; }

    }
}