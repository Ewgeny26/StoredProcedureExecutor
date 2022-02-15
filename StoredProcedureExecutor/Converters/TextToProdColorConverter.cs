using System;
using System.Globalization;
using System.Windows.Data;

namespace StoredProcedureExecutor.Converters
{
    public class TextToProdColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is string)
            {
                var text = (string)value;
                return text.StartsWith("prod", StringComparison.OrdinalIgnoreCase) ? "#c62828" : "#fff";
            }
            throw new InvalidCastException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
