using System;
using System.Globalization;
using System.Linq;

namespace måleusikkerhet.Interface;

public static class NumberParser
{
    public static double? ParseNumber(string? text)
    {
        if (string.IsNullOrEmpty(text)) return null;
        var multiplier = text[^1] switch
        {
            'M' => 1E6,
            'k' => 1E3,
            'm' => 1E-3,
            'u' => 1E-6,
            'μ' => 1E-6,
            'n' => 1E-9,
            'p' => 1E-12,
            _ => 1E0
        };
        var b = GetDouble(Math.Abs(multiplier - 1) < double.Epsilon ? text : text[..^1]);
        if(b.Item2)
            return Math.Round(b.Item1 * multiplier,15);
        return null;
    }
    public static string ParseNumberBack(double? value)
    {
        return value switch
        {
            < 1E-9 => value * 1E12 + "p",
            < 1E-6 => value * 1E9 + "n",
            < 1E-3 => value * 1E6 + "μ",
            < 1E0 => value * 1E3 + "m",
            < 1E3 => value * 1E0 + "",
            < 1E6 => value * 1E-3 + "k",
            < 1E9 => value * 1E-6 + "M",
            _ => value * 1E-9 + "G"
        };
    }
    private static (double,bool) GetDouble(string value)
    {
        double result;

        // Try parsing in the current culture
        if (!double.TryParse(value, NumberStyles.Any, CultureInfo.CurrentCulture, out result) &&
            // Then try in US english
            !double.TryParse(value, NumberStyles.Any, CultureInfo.GetCultureInfo("en-US"), out result) &&
            // Then in neutral language
            !double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out result))
        {
            return (0,false);
        }
        return (result,true);
    }
}