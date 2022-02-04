using System.Collections.Generic;
using FIAT.Backend.DomainLayer.IDataModel;

namespace FIAT.Backend.DomainLayer.DataModel
{
    public class FloodDamageBasinData : BasinData, IFloodDamageBasin
    {
        public IEnumerable<IScenario> Scenarios { get; set; }
    }
}