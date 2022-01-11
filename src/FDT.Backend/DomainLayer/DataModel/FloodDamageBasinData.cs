using System.Collections.Generic;
using FDT.Backend.DomainLayer.IDataModel;

namespace FDT.Backend.DomainLayer.DataModel
{
    public class FloodDamageBasinData : BasinData, IFloodDamageBasin
    {
        public IEnumerable<IScenario> Scenarios { get; set; }
    }
}