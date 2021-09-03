using System.Collections.Generic;
using FDT.Backend.DomainLayer.IDataModel;

namespace FDT.Backend.DomainLayer.DataModel
{
    public class BasinData: IBasin
    {
        public string BasinName { get; set; }
        public string Projection { get; set; }
        public IEnumerable<IScenario> Scenarios { get; set; }
    }
}
