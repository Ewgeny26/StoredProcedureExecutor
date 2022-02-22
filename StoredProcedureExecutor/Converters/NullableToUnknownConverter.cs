using StoredProcedureExecutor.Constants;
using System;
using System.Globalization;
using System.Windows.Data;

namespace StoredProcedureExecutor.Converters
{
    public class NullableToUnknownConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return value?.ToString() ?? StatusMessages.UnknownStatus;
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}