using måleusikkerhet.Infrastructure;
using måleusikkerhet.Interface;

namespace måleusikkerhet.Test;

public class UnitTest1
{
    [Theory]
    [InlineData(111.52,10.256,"112 ± 11")]
    [InlineData(0.1555,0.0156,"0,156 ± 0,016")]
    [InlineData(1.199,1.016,"1,2 ± 1,1")]
    public void TrueTest(double value, double uncertainty, string result)
    {
        Assert.Equal(result,Rounder.RoundedValue(value,uncertainty));
    }
    [Theory]
    [InlineData("10u",0.00001)]
    [InlineData("10m",0.01)]
    [InlineData("10k",10000)]
    [InlineData("47u",0.000047)]
    [InlineData("47m",0.047)]
    [InlineData("47k",47000)]
    [InlineData("47",47)]
    public void NumberInputTest(string? text, double result)
    {
        Assert.Equal(result, NumberParser.ParseNumber(text)!);
    }
    

    [Fact]
    public void NullOutput()
    {
        Assert.Null(NumberParser.ParseNumber("daw"));
    }
}