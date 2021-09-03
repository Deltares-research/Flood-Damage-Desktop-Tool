using System.Collections.Generic;

namespace FDT.Backend.DomainLayer.IDataModel
{
    public interface IScenario
    {
        string ScenarioName { get; }
        IEnumerable<IFloodMapBase> FloodMaps { get; }
    }
}