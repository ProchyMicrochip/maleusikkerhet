using System.Globalization;
using måleusikkerhet.Infrastructure;

namespace måleusikkerhet.Test;

public class PreciseNumberTest
{
    [Theory]
    [InlineData("100m","1","1.1")]
    [InlineData("100m","200m","0.3")]
    [InlineData("100,15m","12m","112.15m")]
    public void AddTest(string first, string second, string expected)
    {
        Assert.Equal(expected,(Precision.Parse(first,null)+Precision.Parse(second,null)).ToString());
    }
    [Theory]
    [InlineData("12m","12m")]
    [InlineData("100m","0,1")]
    [InlineData("12,5k","12,5k")]
    [InlineData("100,15m","100,15m")]
    [InlineData("12,5","12,5")]
    [InlineData("15","15")]
    [InlineData("12k","12k")]
    [InlineData("125.84M","125,84M")]
    [InlineData("1248000949p","1,248000949m")]
    [InlineData("12480000949","12,480000949G")]
    public void ToStringTest(string first, string expected)
    {
        Assert.Equal(expected,Precision.Parse(first,CultureInfo.CurrentCulture).ToString());
    }
    
    [Theory]
    [InlineData("10", "10", "0.1k")]
    [InlineData("10", "25", "0.25k")]
    public void Multiplication(string first, string second,string expected)
    {
        Assert.Equal(expected,(Precision.Parse(first,null)*Precision.Parse(second,null)).ToString());
    }
    [Theory]
    [InlineData("10", "10", "1")]
    [InlineData("10", "25", "0.4")]
    public void Division(string first, string second,string expected)
    {
        Assert.Equal(expected,(Precision.Parse(first,null)/Precision.Parse(second,null)).ToString());
    }

    [Theory]
    [InlineData("1", "1")]
    [InlineData("1000", "10")]
    [InlineData("3", "1,732051")]
    [InlineData("25", "5")]
    public void Sqrt(string number, string result)
    {
        Assert.Equal(result,Precision.Parse(number,null).Sqrt2().ToString());
    }
}