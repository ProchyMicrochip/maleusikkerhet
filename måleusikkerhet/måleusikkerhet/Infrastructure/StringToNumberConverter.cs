using System;
using System.Collections.Generic;
using System.Globalization;
using Avalonia.Data;
using Avalonia.Data.Converters;

namespace måleusikkerhet.Infrastructure;

public class StringToNumberConverter : IMultiValueConverter
{
    public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (values[0] is not Precision || values[1] is not string) return "";
        return (values[0] as Precision)?.ToString() + values[1];
    }
}