using System.Collections.Generic;
using FIAT.Backend.DomainLayer.IDataModel;

namespace FIAT.Backend.DomainLayer.DataModel
{
    public class ScenarioData: IScenario
    {
        public string ScenarioName { get; set; }
        public IEnumerable<IFloodMapBase> FloodMaps { get; set; }
    }
}