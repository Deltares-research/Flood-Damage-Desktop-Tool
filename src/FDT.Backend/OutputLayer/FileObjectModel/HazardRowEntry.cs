using FDT.Backend.IDataModel;

namespace FDT.Backend.OutputLayer.FileObjectModel
{
    public class HazardRowEntry : IRowEntry
    {
        public string HazardFile { get; }
        public object ReturnPeriod { get; }
        public string CRS { get; }
        // public DateTime InundationReference { get; set; }
        public HazardRowEntry(IFloodMapBase floodMapBase, string basinProjection)
        {
            HazardFile = floodMapBase.Path;
            ReturnPeriod = floodMapBase.GetReturnPeriod();
            CRS = basinProjection;
        }
    }
}