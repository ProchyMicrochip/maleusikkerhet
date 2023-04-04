using System;
using System.Globalization;
using System.Linq;
using System.Numerics;
using Avalonia.Media;
using static System.Int64;

namespace måleusikkerhet.Infrastructure;

public class Precision : INumber<Precision>
{
    public long Mantis { get; }
    public int Exponent { get; }
    public Precision(long mantis, int exponent)
    {
        while (mantis % 10 == 0 && mantis != 0)
        {
            mantis /= 10;
            exponent++;
        }
        Mantis = mantis;
        Exponent = exponent;
    }

    public Precision()
    {
        Mantis = 0;
        Exponent = 0;
    }

    public Precision Sqrt2()
    {
        var mantisSqrt = Math.Sqrt(Mantis);
        var tmp = Parse(Math.Round(mantisSqrt,6).ToString(CultureInfo.InvariantCulture),null);
        return new Precision(tmp.Mantis, tmp.Exponent + Exponent/2);
    }

    public Precision Pow2()
    {
        return this * this;
    }

    public int CompareTo(object? obj)
    {
        if (obj == null) throw new ArgumentNullException();
        if (obj.GetType() != typeof(Precision)) throw new InvalidOperationException();
        return CompareTo(obj as Precision);
    }

    public int CompareTo(Precision? other)
    {
        if (other == null) throw new ArgumentNullException();
        if (Exponent > other.Exponent) return 1;
        if (Exponent < other.Exponent) return -1;
        if (Exponent != other.Exponent) throw new InvalidOperationException();
        if (Mantis > other.Mantis) return 1;
        if (Mantis < other.Mantis) return -1;
        if (Mantis == other.Mantis) return 0;
        throw new InvalidOperationException();
    }

    public bool Equals(Precision? other)
    {
        if (other == null) return false;
        return Mantis == other.Mantis && Exponent == other.Exponent;
    }

    public override string ToString()
    {
        return ToString(null, null);
    }
    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        if (this == Zero) return "0";
        var mantis = Mantis.ToString();
        if (Mantis / 1000 == 0)
        {
            switch (Exponent % 3)
            {
                case 0:
                    return Mantis.ToString(formatProvider) + GetSuffix(Exponent);
                case -1:
                case 2:
                {
                    if (mantis.Length == 1)
                        mantis = "0" + mantis;
                    return mantis.Insert(mantis.Length - 1,
                        CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator) + GetSuffix(Exponent + 1);
                }
                case -2:
                case 1:
                {
                    if (mantis.Length < 3)
                    {
                        return mantis + "0" + GetSuffix(Exponent-1);
                    }
                    return mantis.Insert(1, CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator) +
                           GetSuffix(Exponent + 2);
                }
            }
        }
        var modulo = Exponent >= 0 ? Exponent % 3 : (Exponent % 3 + 3) % 3;
        mantis += new string(Enumerable.Repeat('0', modulo).ToArray());
        var digits = mantis.Length;
        var add = digits / 3 +( digits % 3 == 0 ? -1 : 0);

        return mantis.Insert(digits - add * 3, CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator).TrimEnd('0') + GetSuffix(Exponent - modulo + add*3);



    }

    private static string GetSuffix(int exponent) =>
        exponent switch
        {
            <= -9 => "n",
            <= -6 => "μ",
            <= -3 => "m",
            <= 0 => "",
            <= 3 => "k",
            <= 6 => "M",
            <= 9 => "G",
            _ => "Error"
        };

    public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
    {
        throw new NotImplementedException();
    }

    public static Precision Parse(string s, IFormatProvider? provider)
    {
        if (string.IsNullOrEmpty(s)) return new Precision();
        var exponent = s[^1] switch
        {
            'M' => 6,
            'k' => 3,
            'm' => -3,
            'u' => -6,
            'μ' => -6,
            'n' => -9,
            'p' => -12,
            _ => 0
        };
        var index = s.IndexOf(',');
        if (index == -1)
            index = s.IndexOf('.');
        if (index == -1)
            return new Precision(long.Parse(exponent == 0 ? s : s[..^1]),exponent);
        int overflow;
        var ogExponent = exponent;
        if (exponent == 0)
        {
            overflow = s.Length - index -1;
        }
        else
        {
            overflow = s.Length - index -2;
        }
        
        exponent -= overflow;
        s = s.Replace((provider as CultureInfo)?.NumberFormat.NumberDecimalSeparator ??
                  CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator, "");
        s = s.Replace(",", "");
        s = s.Replace(".", "");
        return ogExponent == 0 ? new Precision(long.Parse(s), exponent) : new Precision(long.Parse(s[..^1]), exponent);
    }

    public static bool TryParse(string? s, IFormatProvider? provider, out Precision result)
    {
        result = new Precision();
        if (s == null) return false;
        try
        {
            result = Parse(s, provider);
            return true;
        }
        catch (Exception)
        {
            //Ignored
        }
        return false;
    }

    public static Precision Parse(ReadOnlySpan<char> s, IFormatProvider? provider)
    {
        var exponent = s[^1] switch
        {
            'M' => 6,
            'k' => 3,
            'm' => -3,
            'u' => -6,
            'μ' => -6,
            'n' => -9,
            'p' => -12,
            _ => 0
        };
        var index = s.IndexOf(',');
        if (index == -1)
            index = s.IndexOf('.');
        if (index == -1)
            return new Precision(long.Parse(exponent == 0 ? s : s[..^1]),exponent);
        var overflow = s.Length - index -2;
        exponent -= overflow;
        var b = s.ToString();
        b = b.Replace((provider as CultureInfo)?.NumberFormat.NumberDecimalSeparator ??
                      CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator, "");
        b = b.Replace(",", "");
        b = b.Replace(".", "");
        return new Precision(long.Parse(b[..^1]), exponent);
    }

    public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, out Precision result)
    {
        result = new Precision();
        try
        {
            result = Parse(s, provider);
            return true;
        }
        catch (Exception)
        {
            //Ignored
        }
        return false;
    }

    public static Precision operator +(Precision left, Precision right)
    {
        if (int.Abs(left.Exponent) > int.Abs(right.Exponent))
        {
            var sameExp = right.Mantis * IntPow(Radix, (uint)int.Abs(left.Exponent - right.Exponent));
            return new Precision(left.Mantis + sameExp, left.Exponent);
        }

        if (int.Abs(left.Exponent) >= int.Abs(right.Exponent)) return new Precision(left.Mantis + right.Mantis, left.Exponent);
        {
            var sameExp =left.Mantis * IntPow(Radix, (uint)int.Abs(right.Exponent - left.Exponent));
            return new Precision(right.Mantis + sameExp, right.Exponent);    
        }
    }

    public static Precision AdditiveIdentity { get; } = new(0, 0);
    public static bool operator ==(Precision? left, Precision? right)
    {
        if (left is null || right is null) return false;
        return Equals(left, right);
    }

    public static bool operator !=(Precision? left, Precision? right)
    {
        if (left is null && right is null) return false;
        if (left is null || right is null) return true;
        return !Equals(left, right);
    }

    public static bool operator >(Precision left, Precision right)
    {
        if (left == null || right == null) throw new ArgumentNullException();
        if (left.Exponent > right.Exponent) return true;
        if (left.Exponent < right.Exponent) return false;
        if (left.Exponent != right.Exponent) throw new InvalidOperationException();
        if (left.Mantis > right.Mantis) return true;
        if (left.Mantis < right.Mantis) return false;
        if (left.Mantis == right.Mantis) return false;
        throw new InvalidOperationException();
    }

    public static bool operator >=(Precision left, Precision right)
    {
        if (left == null || right == null) throw new ArgumentNullException();
        if (left.Exponent > right.Exponent) return true;
        if (left.Exponent < right.Exponent) return false;
        if (left.Exponent != right.Exponent) throw new InvalidOperationException();
        if (left.Mantis > right.Mantis) return true;
        if (left.Mantis < right.Mantis) return false;
        if (left.Mantis == right.Mantis) return true;
        throw new InvalidOperationException();
    }

    public static bool operator <(Precision left, Precision right)
    {
        return !(left >= right);
    }

    public static bool operator <=(Precision left, Precision right)
    {
        return !(left > right);
    }

    public static Precision operator --(Precision value)
    {
        return value - new Precision(1, 0);
    }

    public static Precision operator /(Precision left, Precision right)
    {
        // ReSharper disable once PossibleLossOfFraction
        var factor = (int)Math.Floor(Math.Log10(MaxValue / left.Mantis));
        var factored = left.Mantis * LongPow(10, (uint)factor);
        var divided = factored / right.Mantis;
        return new Precision(  divided,left.Exponent-right.Exponent - factor);
    }

    public static Precision operator ++(Precision value)
    {
        return value + new Precision(1, 0);
    }

    public static Precision operator %(Precision left, Precision right)
    {
        throw new NotImplementedException();
    }

    public static Precision MultiplicativeIdentity { get; } = new(1, 0);
    public static Precision operator *(Precision left, Precision right)
    {
        var tmp = (Int128)left.Mantis * right.Mantis;
        var add = 0;
        while ((Int128.IsPositive(tmp) && tmp > MaxValue)||(Int128.IsNegative(tmp) && tmp < MinValue))
        {
            tmp /= 10;
            add++;
        }
        return new Precision(long.Parse(tmp.ToString()), left.Exponent + right.Exponent + add );
    }

    public static Precision operator -(Precision left, Precision right)
    {
        if (left.Exponent > right.Exponent)
        {
            var sameExp = right.Mantis * IntPow(Radix, (uint)(left.Exponent - right.Exponent));
            return new Precision(left.Mantis - sameExp, left.Exponent);
        }

        if (left.Exponent >= right.Exponent) return new Precision(left.Mantis - right.Mantis, left.Exponent);
        {
            var sameExp =left.Mantis * IntPow(Radix, (uint)(right.Exponent - left.Exponent));
            return new Precision(right.Mantis - sameExp, right.Exponent);    
        }
    }

    public static Precision operator -(Precision value)
    {
        return new Precision(-value.Mantis, value.Exponent);
    }

    public static Precision operator +(Precision value)
    {
        return new Precision(value.Mantis, value.Exponent);
    }

    public static Precision Abs(Precision value)
    {
        return value.Mantis < 0
            ? new Precision(-value.Mantis, value.Exponent)
            : new Precision(value.Mantis, value.Exponent);
    }

    public static bool IsCanonical(Precision value) => false;

    public static bool IsComplexNumber(Precision value) => false;

    public static bool IsEvenInteger(Precision value)
    {
        throw new NotImplementedException();
    }

    public static bool IsFinite(Precision value) => true;

    public static bool IsImaginaryNumber(Precision value) => false;

    public static bool IsInfinity(Precision value) => false;

    public static bool IsInteger(Precision value) => value.Exponent >= 0;

    public static bool IsNaN(Precision value) => false;

    public static bool IsNegative(Precision value) => value.Mantis < 0;

    public static bool IsNegativeInfinity(Precision value) => false;

    public static bool IsNormal(Precision value) => true;

    public static bool IsOddInteger(Precision value)
    {
        throw new NotImplementedException();
    }

    public static bool IsPositive(Precision value) => value.Mantis > 0;

    public static bool IsPositiveInfinity(Precision value) => false;

    public static bool IsRealNumber(Precision value) => true;

    public static bool IsSubnormal(Precision value) => false;

    public static bool IsZero(Precision value) => value.Mantis == 0;

    public static Precision MaxMagnitude(Precision x, Precision y) => x > y ? x : y;

    public static Precision MaxMagnitudeNumber(Precision x, Precision y) => x > y ? x : y;

    public static Precision MinMagnitude(Precision x, Precision y) => x < y ? x : y;

    public static Precision MinMagnitudeNumber(Precision x, Precision y)  => x < y ? x : y;

    public static Precision Parse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider)
    {
        return Parse(s,provider);
    }

    public static Precision Parse(string s, NumberStyles style, IFormatProvider? provider)
    {
        throw new NotImplementedException();
    }

    public static bool TryConvertFromChecked<TOther>(TOther value, out Precision result) where TOther : INumberBase<TOther>
    {
        throw new NotImplementedException();
    }

    public static bool TryConvertFromSaturating<TOther>(TOther value, out Precision result) where TOther : INumberBase<TOther>
    {
        throw new NotImplementedException();
    }

    public static bool TryConvertFromTruncating<TOther>(TOther value, out Precision result) where TOther : INumberBase<TOther>
    {
        throw new NotImplementedException();
    }

    public static bool TryConvertToChecked<TOther>(Precision value, out TOther result) where TOther : INumberBase<TOther>
    {
        throw new NotImplementedException();
    }

    public static bool TryConvertToSaturating<TOther>(Precision value, out TOther result) where TOther : INumberBase<TOther>
    {
        throw new NotImplementedException();
    }

    public static bool TryConvertToTruncating<TOther>(Precision value, out TOther result) where TOther : INumberBase<TOther>
    {
        throw new NotImplementedException();
    }

    public static bool TryParse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider, out Precision result)
    {
        throw new NotImplementedException();
    }

    public static bool TryParse(string? s, NumberStyles style, IFormatProvider? provider, out Precision result)
    {
        throw new NotImplementedException();
    }

    public static Precision One { get; } = new(1, 0);
    public static int Radix => 10;
    public static Precision Zero { get; } = new(0, 0);
    private static int IntPow(int x, uint pow)
    {
        var ret = 1;
        while ( pow != 0 )
        {
            if ( (pow & 1) == 1 )
                ret *= x;
            x *= x;
            pow >>= 1;
        }
        return ret;
    }
    private static long LongPow(long x, long pow)
    {
        if (pow == 0) return x*1;
        var result = x;
        for (var i = 0; i < pow-1; i++)
        {
            result *= x;
        }
        return result;
    }
}