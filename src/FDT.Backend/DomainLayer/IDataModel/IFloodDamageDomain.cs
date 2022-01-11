namespace FDT.Backend.DomainLayer.IDataModel
{
    public interface IFloodDamageDomain
    {
        IFloodDamageBasin FloodDamageBasinData { get; set; }
        IApplicationPaths Paths { get; set; }
    }
}