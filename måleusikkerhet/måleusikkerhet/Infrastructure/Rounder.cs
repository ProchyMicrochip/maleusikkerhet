using System;
using System.Globalization;
using System.Linq;
using Avalonia.Media;

namespace måleusikkerhet.Infrastructure;

public static class Rounder
{
    public static string RoundedValue(double value, double uncertanty)
    {
        var tmp = uncertanty.ToString(NumberFormatInfo.InvariantInfo);
        var decimalpoint = tmp.IndexOf('.');
        switch (tmp[0])
        {
            case '0':
            {
                var first = tmp.IndexOfAny(new[] { '1', '2', '3', '4', '5', '6', '7', '8', '9' });
                if (tmp[first] is '1' or '2') first++;
                uncertanty = RoundCeiling(uncertanty, first - decimalpoint);
                value = Round(value, first - decimalpoint);
                break;
            }
            case '1' or '2':
                uncertanty = RoundCeiling(uncertanty, -decimalpoint+2);
                value = Round(value, -decimalpoint + 2);
                break;
            default:
                uncertanty = RoundCeiling(uncertanty, -decimalpoint+1);
                value = Round(value, -decimalpoint + 1);
                break;
        }
        return $"{value} ± {uncertanty}";
    }

    private static double RoundCeiling(double value, int decimalplaces)
    {
        var multiplier = Math.Pow(10, decimalplaces);
        return Math.Ceiling(value * multiplier) / multiplier;
    }
    private static double Round(double value, int decimalplaces)
    {
        var multiplier = Math.Pow(10, decimalplaces);
        var tmp =(int)(value * multiplier * 10);
        var a = tmp % 10;
        var b = tmp / 10 % 2;
        if (a == 5 && b == 0)
            return Math.Floor(value * multiplier);
        return Math.Round(value * multiplier) / multiplier;
    }
}