using StoredProcedureExecutor.Constants;
using System;
using System.Globalization;
using System.Windows.Data;

namespace StoredProcedureExecutor.Converters
{
    public class LocalDateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return StatusMessages.UnknownStatus;
            if (value is DateTime)
            {
                var dateTime = (DateTime)value;
                return dateTime.ToLocalTime().ToString();
            }
            throw new InvalidCastException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
