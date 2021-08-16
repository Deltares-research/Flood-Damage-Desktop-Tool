using System.Collections.Generic;
using FDT.Backend.IDataModel;

namespace FDT.Backend.DataModel
{
    public class BasinData: IBasin
    {
        public ScenarioType NameScenario { get; set; }
        public IEnumerable<IScenario> Scenarios { get; set; }
    }
}
