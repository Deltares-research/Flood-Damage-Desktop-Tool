using System.Collections.Generic;
using FDT.Backend.DomainLayer.IDataModel;

namespace FDT.Backend.DomainLayer.DataModel
{
    public class ScenarioData: IScenario
    {
        public string ScenarioName { get; set; }
        public IEnumerable<IFloodMapBase> FloodMaps { get; set; }
    }
}