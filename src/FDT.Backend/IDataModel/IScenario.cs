using System.Collections.Generic;

namespace FDT.Backend.IDataModel
{
    public interface IScenario
    {
        string ScenarioName { get; set; }
        IEnumerable<IFloodMap> FloodMaps { get; set; }
    }
}