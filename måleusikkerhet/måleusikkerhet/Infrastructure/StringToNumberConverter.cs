using System;
using System.Globalization;
using Avalonia.Data.Converters;
using måleusikkerhet.Interface;

namespace måleusikkerhet.Infrastructure;

public class StringToNumberConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return NumberParser.ParseNumberBack(value as double?);
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return NumberParser.ParseNumber(value as string);
    }
}