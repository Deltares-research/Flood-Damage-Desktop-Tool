using System;
using FDT.Backend.IDataModel;
using FDT.Backend.OutputLayer.IFileObjectModel;

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
            if (floodMapBase == null)
                throw new ArgumentNullException(nameof(floodMapBase));
            if (string.IsNullOrEmpty(basinProjection))
                throw new ArgumentNullException(nameof(basinProjection));

            HazardFile = floodMapBase.Path;
            ReturnPeriod = floodMapBase.GetReturnPeriod();
            CRS = basinProjection;
        }
    }
}