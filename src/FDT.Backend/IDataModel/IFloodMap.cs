namespace FDT.Backend.IDataModel
{
    public interface IFloodMap
    {
        string Path { get; set; }
    }

    public interface IFloodMapWithReturnPeriod : IFloodMap
    {
        int ReturnPeriod { get; set; }
    }
}