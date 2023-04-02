using System;
using System.Collections.Generic;
using System.Globalization;
using Avalonia.Data.Converters;

namespace måleusikkerhet.Infrastructure;

public class StringToAtrNumberConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not Precision precision) return null;
        return precision + (parameter as string);
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not string precision || parameter is not string suffix) return "";
        try
        {
            if (precision.Contains(suffix))
                return  string.IsNullOrWhiteSpace(precision[..^suffix.Length]) ? null : Precision.Parse(precision[..^suffix.Length], null);
            return string.IsNullOrWhiteSpace(precision) ? null : Precision.Parse(precision, null);
        }
        catch (Exception e)
        {
            return null;
        }
        
        
    }
}