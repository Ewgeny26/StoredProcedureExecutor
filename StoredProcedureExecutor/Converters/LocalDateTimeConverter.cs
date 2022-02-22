using StoredProcedureExecutor.Constants;
using System;
using System.Globalization;
using System.Windows.Data;

namespace StoredProcedureExecutor.Converters
{
    public class LocalDateTimeConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return value switch
            {
                null => StatusMessages.UnknownStatus,
                DateTime dateTime => dateTime.ToLocalTime(),
                _ => throw new InvalidCastException()
            };
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}