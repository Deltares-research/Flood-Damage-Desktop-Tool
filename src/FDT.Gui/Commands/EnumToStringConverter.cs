using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace FIAT.Gui.Commands
{
    public abstract class EnumToStringConverter : MarkupExtension, IValueConverter
    {
        protected abstract string ConvertToString(object value);

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));
            return ConvertToString(value);
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