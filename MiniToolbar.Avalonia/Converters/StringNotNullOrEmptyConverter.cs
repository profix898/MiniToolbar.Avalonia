using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace MiniToolbar.Avalonia.Converters;

public class StringNotNullOrEmptyConverter : IValueConverter
{
    #region Implementation of IValueConverter

    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return !String.IsNullOrEmpty(value as string);
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }

    #endregion
}
