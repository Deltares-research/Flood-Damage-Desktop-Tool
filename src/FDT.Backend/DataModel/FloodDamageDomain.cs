using FDT.Backend.IDataModel;

namespace FDT.Backend.DataModel
{
    public class FloodDamageDomain: IFloodDamageDomain
    {
        public IBasin BasinData { get; set; }
        public IApplicationPaths Paths { get; set; }

        public FloodDamageDomain()
        {
            BasinData = new BasinData();
            Paths = new ApplicationPaths();
        }
    }
}