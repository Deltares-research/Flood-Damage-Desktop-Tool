namespace FDT.Backend.IDataModel
{
    public interface IFloodDamageDomain
    {
        IBasin BasinData { get; set; }
        IApplicationPaths Paths { get; set; }
    }
}