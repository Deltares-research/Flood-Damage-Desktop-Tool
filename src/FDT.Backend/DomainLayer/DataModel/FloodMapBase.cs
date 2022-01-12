using FDT.Backend.DomainLayer.IDataModel;

namespace FDT.Backend.DomainLayer.DataModel
{
    public class FloodMap : IFloodMap
    {
        public string Path { get; set; }
        public FloodMapType MapType { get; set; }

        public string ReturnPeriod => "Event";
        public object GetReturnPeriod()
        {
            return ReturnPeriod;
        }
    }

    public class FloodMapWithReturnPeriod : IFloodMapWithReturnPeriod
    {
        public string Path { get; set; }
        public FloodMapType MapType { get; set; }
        public int ReturnPeriod { get; set; }
        public object GetReturnPeriod()
        {
            return ReturnPeriod;
        }
    }
}