using System.ComponentModel.DataAnnotations;


namespace FDT.Backend.DomainLayer.IDataModel
{
    public enum FloodMapType
    {
        [Display(Name = "Water depth")]
        WaterDepth,
        [Display(Name = "Water level")]
        WaterLevel
    }
}