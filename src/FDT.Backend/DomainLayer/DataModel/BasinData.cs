using FIAT.Backend.DomainLayer.IDataModel;

namespace FIAT.Backend.DomainLayer.DataModel
{
    public class BasinData: IBasin
    {
        public string BasinName { get; set; }
        public string Projection { get; set; }
    }
}
