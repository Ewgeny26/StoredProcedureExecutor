using System;
using System.Globalization;
using System.Windows.Data;

namespace StoredProcedureExecutor.Converters
{
    public class TextToProdColorConverter : IValueConverter
    {
        private const string DefaultTextColor = "#fff";
        private const string ProdTextColor = "#c62828";

        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is string text)
            {
                return text.StartsWith("prod", StringComparison.OrdinalIgnoreCase) ? ProdTextColor : DefaultTextColor;
            }

            throw new InvalidCastException();
        }

        public object ConvertBack(object? value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}