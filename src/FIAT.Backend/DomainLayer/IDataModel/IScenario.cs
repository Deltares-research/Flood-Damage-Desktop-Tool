using System.Collections.Generic;

namespace FIAT.Backend.DomainLayer.IDataModel
{
    public interface IScenario
    {
        string ScenarioName { get; }
        IEnumerable<IFloodMapBase> FloodMaps { get; }
    }
}