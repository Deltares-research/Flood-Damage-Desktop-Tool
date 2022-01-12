using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using System.Windows.Markup;
using FDT.Backend.DomainLayer.IDataModel;

namespace FDT.Gui.Commands
{
    [ValueConversion(typeof(Enum), typeof(IEnumerable<string>))]
    public class FloodMapTypeStringConverter: MarkupExtension, IValueConverter
    {
        private T Convert<T>(object o)
        {
            T enumVal = (T)Enum.ToObject(typeof(T), o);
            return enumVal;
        }

        private IEnumerable<string> ConvertEnumCollection(IEnumerable<FloodMapType> valueCollection)
        {
            return valueCollection.Select(mapType => Convert<FloodMapType>(mapType).GetDisplayName());
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value)
            {
                case null:
                    throw new ArgumentNullException(nameof(value));
                case string stringValue:
                    return ConvertBack(stringValue, targetType, parameter, culture);
                case IEnumerable<FloodMapType> enumValues:
                    return ConvertEnumCollection(enumValues);
                default:
                    throw new ArgumentNullException(nameof(value));
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}