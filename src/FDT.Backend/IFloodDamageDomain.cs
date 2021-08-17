using FDT.Backend.IDataModel;

namespace FDT.Backend
{
    public interface IFloodDamageDomain
    {
        IBasin BasinData { get; set; }
        IApplicationPaths Paths { get; set; }
    }
}