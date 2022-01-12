using System.ComponentModel.DataAnnotations;


namespace FDT.Backend.DomainLayer.IDataModel
{
    public enum FloodMapType
    {
        [Display(Name = "Water Depth")]
        WaterDepth,
        [Display(Name = "Water Level")]
        WaterLevel
    }
}