using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using FDT.Backend.DomainLayer.IDataModel;

namespace FDT.Gui.Commands
{

    public class FloodMapTypeStringConverter: MarkupExtension, IValueConverter
    {
        private T Convert<T>(object o)
        {
            T enumVal = (T)Enum.ToObject(typeof(T), o);
            return enumVal;
        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));
            FloodMapType floodMapType = Convert<FloodMapType>(value);
            return floodMapType.GetDisplayName();
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