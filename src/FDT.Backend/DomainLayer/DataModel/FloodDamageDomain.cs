using FIAT.Backend.DomainLayer.IDataModel;

namespace FIAT.Backend.DomainLayer.DataModel
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