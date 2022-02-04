using System;
using FIAT.Backend.DomainLayer.IDataModel;

namespace FIAT.Gui.Commands
{

    public class FloodMapTypeStringConverter: EnumToStringConverter
    {
        protected override string ConvertToString(object value)
        {
            FloodMapType enumVal = (FloodMapType)Enum.ToObject(typeof(FloodMapType), value);
            switch (enumVal)
            {
                case FloodMapType.WaterDepth:
                    return Resources.FloodMapTypeStringConverter_Convert_Water_depth;
                case FloodMapType.WaterLevel:
                    return Resources.FloodMapTypeStringConverter_Convert_Water_level;
                default:
                    throw new ArgumentNullException(nameof(enumVal));
            }
        }
    }
}