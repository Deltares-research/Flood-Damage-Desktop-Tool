using System.Collections.Generic;
using FDT.Backend.DataModel;

namespace FDT.Backend.IDataModel
{
    public interface IBasin
    {
        ScenarioType NameScenario { get; set; }
        IEnumerable<IScenario> Scenarios { get; set; }
    }
}