using måleusikkerhet.Infrastructure;

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
}