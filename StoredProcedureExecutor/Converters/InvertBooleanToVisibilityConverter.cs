using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace StoredProcedureExecutor.Converters
{
    public class InvertBooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is bool booleanValue)
            {
                return booleanValue ? Visibility.Hidden : Visibility.Visible;
            }

            throw new InvalidCastException();
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}