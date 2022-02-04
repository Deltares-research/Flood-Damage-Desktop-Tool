namespace FIAT.Backend.DomainLayer.IDataModel
{
    public interface IFloodMapBase
    {
        string Path { get; set; }
        FloodMapType MapType { get; set; }
        object GetReturnPeriod();
    }

    public interface IFloodMap: IFloodMapBase
    {
        string ReturnPeriod { get; }
    }

    public interface IFloodMapWithReturnPeriod : IFloodMapBase
    {
        int ReturnPeriod { get; set; }
    }
}