using System;
using System.Globalization;
using System.Linq;

namespace måleusikkerhet.Interface;

public static class NumberParser
{
    public static double? ParseNumber(string text)
    {
        var multiplier = text[^1] switch
        {
            'M' => 1E6,
            'k' => 1E3,
            'm' => 1E-3,
            'u' => 1E-6,
            'n' => 1E-9,
            'p' => 1E-12,
            _ => 1E0
        };
        if(double.TryParse(Math.Abs(multiplier - 1) < double.Epsilon ? text : text[..^1], NumberStyles.Any, CultureInfo.InvariantCulture,
            out var result))
            return Math.Round(result * multiplier,15);
        return null;
    }
}