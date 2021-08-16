using FDT.Backend.IDataModel;

namespace FDT.Backend.DataModel
{
    public class FloodMap: IFloodMap
    {
        public string Path { get; set; }
    }

    public class FloodMapWithReturnPeriod : IFloodMapWithReturnPeriod
    {
        public string Path { get; set; }
        public int ReturnPeriod { get; set; }
    }
}