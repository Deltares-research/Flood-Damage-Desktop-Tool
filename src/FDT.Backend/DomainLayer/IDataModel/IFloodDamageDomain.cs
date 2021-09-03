namespace FDT.Backend.DomainLayer.IDataModel
{
    public interface IFloodDamageDomain
    {
        IBasin BasinData { get; set; }
        IApplicationPaths Paths { get; set; }
    }
}