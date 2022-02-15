﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace StoredProcedureExecutor.Converters
{
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool)
            {
                var booleanValue = (bool)value;
                return booleanValue ? Visibility.Visible : Visibility.Collapsed;
            }
            throw new InvalidCastException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}