using FDT.Backend.DomainLayer.IDataModel;

namespace FDT.Backend.DomainLayer.DataModel
{
    public class FloodDamageDomain: IFloodDamageDomain
    {
        public IFloodDamageBasin FloodDamageBasinData { get; set; }
        public IApplicationPaths Paths { get; set; }

        public FloodDamageDomain()
        {
            FloodDamageBasinData = new FloodDamageBasinData();
            Paths = new ApplicationPaths();
        }
    }
}