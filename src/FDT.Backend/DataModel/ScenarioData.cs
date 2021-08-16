using System.Collections.Generic;
using FDT.Backend.IDataModel;

namespace FDT.Backend.DataModel
{
    public class ScenarioData: IScenario
    {
        public string ScenarioName { get; set; }
        public IEnumerable<IFloodMapBase> FloodMaps { get; set; }
    }
}