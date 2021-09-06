using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using FDT.Gui.ViewModels;

namespace FDT.Gui.Commands
{
    public class RunStatusEnumConverter: MarkupExtension, IValueConverter
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
            return Convert<RunDamageAssessmentStatusEnum>(value).GetDisplayName();
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