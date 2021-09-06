using System.Collections.Generic;

namespace FDT.Backend.DomainLayer.IDataModel
{
    public interface IBasin
    {
        string BasinName { get; set; }
        string Projection { get; set; }
        IEnumerable<IScenario> Scenarios { get; set; }
    }
}