using FDT.Backend.IDataModel;

namespace FDT.Backend
{
    public class FloodDamageDomain
    {
        public IBasin BasinData { get; set; }
        public IApplicationPaths Paths { get; set; }
    }
}