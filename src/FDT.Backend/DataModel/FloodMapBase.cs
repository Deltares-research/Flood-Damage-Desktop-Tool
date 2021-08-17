using FDT.Backend.IDataModel;

namespace FDT.Backend.DataModel
{
    public class FloodMap : IFloodMap
    {
        public string Path { get; set; }

        public string ReturnPeriod => "Event";
        public object GetReturnPeriod()
        {
            return ReturnPeriod;
        }
    }

    public class FloodMapBaseWithReturnPeriod : IFloodMapBaseWithReturnPeriod
    {
        public string Path { get; set; }
        public int ReturnPeriod { get; set; }
        public object GetReturnPeriod()
        {
            return ReturnPeriod;
        }
    }
}