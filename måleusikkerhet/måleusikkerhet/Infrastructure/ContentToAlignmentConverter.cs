using System;
using System.Globalization;
using Avalonia.Controls;
using Avalonia.Data.Converters;
using Avalonia.Layout;

namespace måleusikkerhet.Infrastructure;

public class ContentToAlignmentConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return (value as TextBox)?.Text is "V" or "A" or "Hz" or "Ω" or "-" ? HorizontalAlignment.Left : HorizontalAlignment.Right;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return null;
    }
}