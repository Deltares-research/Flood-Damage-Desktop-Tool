using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using FIAT.Gui.ViewModels;

namespace FIAT.Gui.Commands
{
    public class AssessmentStatusVisibilityConverter: MarkupExtension, IValueConverter
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

            return Convert<AssessmentStatus>(value) == AssessmentStatus.LoadingBasins ? Visibility.Visible : Visibility.Hidden;
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