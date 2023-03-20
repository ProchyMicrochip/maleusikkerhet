using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace måleusikkerhet.Infrastructure;

public class StringToNumberConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return (value as Precision)?.ToString();
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return Precision.Parse(value as string ?? "0",CultureInfo.InvariantCulture);
    }
}